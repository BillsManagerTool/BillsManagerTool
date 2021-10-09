namespace BillsManagement.Business.Contracts.ServiceContracts
{
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.HTTP.Auth.Authenticate;
    using System;
    using System.Collections.Generic;

    public interface IAuthService
    {
        RegisterResponse Register(RegisterRequest request);
        AuthenticateResponse Authenticate(AuthenticateRequest request, string ipAddress);
        DomainModel.Occupant GetOccupantById(Guid id);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void SendRegisterInvitation(Guid occupantId, List<string> emails);

        void RegisterOccupant(RegisterOccupantRequest request);
    }
}
