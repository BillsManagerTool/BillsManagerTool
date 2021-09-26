namespace BillsManagement.Business.Contracts.ServiceContracts
{
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.HTTP.Auth.Authenticate;

    public interface IAuthService
    {
        RegisterResponse Register(RegisterRequest request);
        AuthenticateResponse Authenticate(AuthenticateRequest request, string ipAddress);
        DomainModel.Occupant GetOccupantById(int id);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        GenerateRegisterLinkResponse GenerateRegisterLink(int occupantId);
    }
}
