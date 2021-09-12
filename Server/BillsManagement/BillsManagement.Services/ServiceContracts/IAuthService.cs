namespace BillsManagement.Services.ServiceContracts
{
    using BillsManagement.DataContracts.Auth;

    public interface IAuthService
    {
        RegisterResponse Register(RegisterRequest request);
        AuthenticateResponse Authenticate(AuthenticateRequest request, string ipAddress);
        DomainModel.Occupant GetOccupantById(int id);
    }
}
