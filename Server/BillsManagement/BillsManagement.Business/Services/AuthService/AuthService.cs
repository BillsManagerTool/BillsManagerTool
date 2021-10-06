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

    public partial class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtUtils _jwtUtils;

        public AuthService(IAuthRepository authRepository, IJwtUtils jwtUtils)
        {
            this._authRepository = authRepository;
            this._jwtUtils = jwtUtils;
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

            var settings = this._authRepository.GetNotificationSettings(1);
            this.SendRegisterNotificationEmail(request.Email, settings);

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

            // build the query string params
            var queryString = this.BuildQueryString(registerLinkDetails);
            response.QueryString = HttpUtility.UrlDecode(queryString); // ??

            return response;
        }
    }
}
