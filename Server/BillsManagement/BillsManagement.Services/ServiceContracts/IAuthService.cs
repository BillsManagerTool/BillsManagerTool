namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DataContracts.Auth;

    public interface IAuthService
    {
        RegisterResponse Register(RegisterRequest request);
        LoginResponse Authenticate(LoginRequest request, string ipAddress);
        DomainModel.Occupant GetOccupantById(int id);
    }
}
