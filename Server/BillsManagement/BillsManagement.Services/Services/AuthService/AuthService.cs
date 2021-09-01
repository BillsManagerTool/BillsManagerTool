namespace BillsManagement.Services.Services.AuthService
{
    using BillsManagement.DataContracts.Args;
    using BillsManagement.DataContracts.Auth;
    using BillsManagement.DomainModel;
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility;
    using Microsoft.Extensions.Options;
    using System;
    using System.Net;

    public partial class AuthService : IAuthService
    {
        private readonly Secrets _secrets;
        private readonly IAuthRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthService(IOptions<Secrets> secrets, IAuthRepository userRepository, IAuthorizationRepository authorizationRepository)
        {
            this._secrets = secrets.Value ?? throw new ArgumentException(nameof(secrets));
            this._userRepository = userRepository;
            this._authorizationRepository = authorizationRepository;
        }

        public LoginResponse Login(LoginRequest request)
        {
            DomainModel.OccupantDetails occupantDetails = this._userRepository
                .GetOccupantDetails(request.Email);

            PasswordCipher.Decrypt(occupantDetails.Password, request.Password);

            DomainModel.SecurityToken token = this._userRepository
                .GetSecurityTokenByOccupantId(occupantDetails.OccupantId);

            DomainModel.TokenValidator tokenValidator = new DomainModel.TokenValidator();
            tokenValidator.SecurityToken = token;
            tokenValidator.Occupant = occupantDetails;

            var securityToken = this.GetValidToken(tokenValidator);

            LoginResponse response = new LoginResponse();
            response.Token = securityToken;

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

            this._userRepository.Register(registerArg);

            var settings = this._userRepository.GetNotificationSettings(1);
            this.SendRegisterNotificationEmail(request.Email, settings);

            RegisterResponse response = new RegisterResponse();
            return response;
        }

        public void ValidateJwtToken(int occupantId)
        {
            SecurityToken authorization = this._userRepository.GetSecurityTokenByOccupantId(occupantId);

            if (authorization.ExpirationDate <= DateTime.Now)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, GlobalConstants.UnauthorizedMessage);
            }
        }
    }
}
