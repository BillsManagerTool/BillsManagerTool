namespace BillsManagement.Business.Services.AuthService
{
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Contracts.Args;
    using BillsManagement.DomainModel;
    using BillsManagement.Custom.CustomExceptions;
    using BillsManagement.Security;
    using System.Collections.Generic;
    using System.Net;
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.HTTP.Auth.Authenticate;
    using System.Web;
    using Microsoft.Extensions.Options;
    using BillsManagement.Utility.Notifications;
    using System.Linq;

    public partial class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IRegisterLinkUtils _registerLinkUtils;
        private readonly IOptions<ApplicationSettings> _appSettings;

        public AuthService(IAuthRepository authRepository, 
            IJwtUtils jwtUtils, 
            IRegisterLinkUtils registerLinkUtils,
            IOptions<ApplicationSettings> appSettings)
        {
            this._authRepository = authRepository;
            this._jwtUtils = jwtUtils;
            this._registerLinkUtils = registerLinkUtils;
            this._appSettings = appSettings;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request, string ipAddress)
        {
            OccupantDetails occupantDetails = this._authRepository.GetOccupantDetails(request.Email);

            if (occupantDetails == null)
            {
                string msg = "User is not valid.";
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, msg);
            }
            PasswordCipher.Decrypt(occupantDetails.Password, request.Password);

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = this._jwtUtils.GenerateJwtToken(occupantDetails);
            var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);

            this._authRepository.SaveRefreshToken(occupantDetails.OccupantDetailsId, refreshToken);

            // remove old inactive refresh tokens from user based on TTL in app settings
            this._authRepository.RemoveOldRefreshTokens(occupantDetails.OccupantDetailsId);

            AuthenticateResponse response = new AuthenticateResponse();
            response.Token = jwtToken;
            response.Email = occupantDetails.Email;
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var occupantRefreshToken = this._authRepository.GetOccupantDetailsByRefreshToken(token);

            if (occupantRefreshToken.RefreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                this.RevokeDescendantRefreshTokens(occupantRefreshToken.RefreshToken, 
                    occupantRefreshToken.OccupantDetails, 
                    ipAddress, 
                    $"Attempted reuse of revoked ancestor token: {token}");
            }

            if (!occupantRefreshToken.RefreshToken.IsActive)
            {
                string msg = "Invalid token";
                throw new HttpStatusCodeException(HttpStatusCode.NotAcceptable, msg);
            }

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = RotateRefreshToken(occupantRefreshToken.RefreshToken, ipAddress);
            newRefreshToken.OccupantDetailsId = occupantRefreshToken.OccupantDetails.OccupantDetailsId;
            this._authRepository.ReplaceRefreshToken(newRefreshToken);

            // remove old inactive refresh tokens from user based on TTL in app settings
            this._authRepository.RemoveOldRefreshTokens(occupantRefreshToken.OccupantDetails.OccupantDetailsId);

            // generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(occupantRefreshToken.OccupantDetails);

            return new AuthenticateResponse(occupantRefreshToken.OccupantDetails.Email, jwtToken, newRefreshToken.Token);
        }

        //public void RevokeToken(string token, string ipAddress)
        //{
        //    var user = getUserByRefreshToken(token);
        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    if (!refreshToken.IsActive)
        //        throw new AppException("Invalid token");

        //    // revoke token and save
        //    revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
        //    _context.Update(user);
        //    _context.SaveChanges();
        //}

        public RegisterResponse Register(RegisterRequest request)
        {
            this.ValidateOccupantExistence(request.Email);
            var encryptedPassword = PasswordCipher.Encrypt(request.Password);

            var registerArgs = new RegisterArgument()
            {
                Email = request.Email,
                Password = encryptedPassword,
                BuildingAddress = request.BuildingAddress,
                EntranceNumber = request.EntranceNumber,
                CountryId = request.CountryId,
                TownId = request.TownId,
                ApartmentFloor = request.ApartmentFloor,
                ApartmentNumber = request.ApartmentNumber
            };

            this._authRepository.Register(registerArgs);
            this._authRepository.RegisterBuilding(registerArgs);

            string subject = "REGISTRATION SUCCESS!";
            string body = this.CreateNotificationMessage();
            this.SendEmailNotification(request.Email, subject, body);

            RegisterResponse response = new RegisterResponse();
            return response;
        }

        public Occupant GetOccupantById(int id)
        {
            var occupant = this._authRepository.GetOccupantById(id);
            if (occupant == null)
                throw new KeyNotFoundException("Occupant not found");

            return occupant;
        }

        public GenerateRegisterLinkResponse GenerateRegisterLink(int occupantId)
        {
            GenerateRegisterLinkResponse response = new GenerateRegisterLinkResponse();

            // prepare information needed
            var registerLinkDetails = this._authRepository.GetRegisterLinkDetails(occupantId);

            var token = this._registerLinkUtils.GenerateRegisterToken(occupantId, 
                registerLinkDetails.BuildingId, 
                registerLinkDetails.EntranceId);


            // build the query string params
            var registerTokenQueryParam = this.BuildRegisterQueryString(token);
            var clientUrl = this._appSettings.Value.Client_URL;
            var encodedRegisterLink = this.BuildRegisterLink(registerTokenQueryParam, clientUrl);
            var registerLink = HttpUtility.UrlDecode(encodedRegisterLink);

            EmailTemplate template = new EmailTemplate();
            var invitationLinkBody = template.RegisterInvitationLinkTemplate(registerLink);

            // TODO
            List<string> emails = new List<string>();
            var subject = "REGISTER INVITATION";
            foreach (var email in emails)
            {
                this.SendEmailNotification(email, subject, invitationLinkBody);
            }

            response.RegisterLink = HttpUtility.UrlDecode(registerLink); // ??

            return response;
        }
    }
}
