namespace BillsManagement.Services.Services.AuthService
{
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Contracts.Args;
    using BillsManagement.DataContracts.Auth;
    using BillsManagement.DomainModel;
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility.Security;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Net;

    public partial class AuthService : IAuthService
    {
        private readonly Secrets _secrets;
        private readonly IAuthRepository _authRepository;
        private readonly IJwtUtils _jwtUtils;

        public AuthService(IOptions<Secrets> secrets,
            IAuthRepository authRepository,
            IJwtUtils jwtUtils)
        {
            this._secrets = secrets.Value ?? throw new ArgumentException(nameof(secrets));
            this._authRepository = authRepository;
            this._jwtUtils = jwtUtils;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request, string ipAddress)
        {
            OccupantDetails occupantDetails = this._authRepository.GetOccupantDetails(request.Email);

            // validate
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

            // remove old refresh tokens from user
            //removeOldRefreshTokens(user);

            AuthenticateResponse response = new AuthenticateResponse();
            response.Token = jwtToken;
            response.Email = occupantDetails.Email;
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        public RegisterResponse Register(RegisterRequest request)
        {
            this.ValidateOccupantExistence(request.Email);
            var encryptedPassword = PasswordCipher.Encrypt(request.Password);

            var registerArg = new RegisterArgument()
            {
                Email = request.Email,
                Password = encryptedPassword
            };

            this._authRepository.Register(registerArg);

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
    }
}
