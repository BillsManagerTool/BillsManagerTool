namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DomainModel;

    public interface IUserService
    {
        DomainModel.RegisterResponse Register(RegisterRequest request);

        DomainModel.LoginResponse Login(LoginRequest request);

        void ValidateJwtToken(int occupantId);
    }
}
