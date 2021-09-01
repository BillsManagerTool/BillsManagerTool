namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DataContracts.Auth;

    public interface IAuthService
    {
        RegisterResponse Register(RegisterRequest request);

        LoginResponse Login(LoginRequest request);

        void ValidateJwtToken(int occupantId);
    }
}
