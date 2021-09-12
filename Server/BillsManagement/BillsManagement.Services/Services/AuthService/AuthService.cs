namespace BillsManagement.Services.Services.AuthService
{
    using BillsManagement.DataContracts.Args;
    using BillsManagement.DataContracts.Auth;
    using BillsManagement.Repository.RepositoryContracts;
    using BillsManagement.Security;
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility.Security;
    using Microsoft.Extensions.Options;
    using System;

    public partial class AuthService : IAuthService
    {
        private readonly Secrets _secrets;
        private readonly IAuthRepository _authRepository;

        public AuthService(IOptions<Secrets> secrets,
            IAuthRepository authRepository)
        {
            this._secrets = secrets.Value ?? throw new ArgumentException(nameof(secrets));
            this._authRepository = authRepository;
        }

        public AuthService() { }

        public LoginResponse Authenticate(LoginRequest request, string ipAddress)
        {
            DomainModel.OccupantDetails occupantDetails = this._authRepository.GetOccupantDetails(request.Email);

            // validate
            PasswordCipher.Decrypt(occupantDetails.Password, request.Password);

            JwtUtils jwt = new JwtUtils(occupantDetails.OccupantId);

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = jwt.GenerateJwtToken(occupantDetails);
            var refreshToken = jwt.GenerateRefreshToken(ipAddress);

            this._authRepository.SaveRefreshToken(occupantDetails.OccupantDetailsId, refreshToken);

            // remove old refresh tokens from user
            //removeOldRefreshTokens(user);

            LoginResponse response = new LoginResponse()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token
            };

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
    }
}
