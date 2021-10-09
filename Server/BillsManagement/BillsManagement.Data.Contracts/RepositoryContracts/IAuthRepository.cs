namespace BillsManagement.Data.Contracts
{
    using BillsManagement.Data.Contracts.Args;
    using BillsManagement.DomainModel.Auth;
    using System;

    public interface IAuthRepository : IBaseRepository<DomainModel.Occupant>
    {
        void RegisterHousekeeper(RegisterArgument args);
        bool IsExistingOccupant(string email);
        DomainModel.OccupantDetails GetOccupantDetails(string email);
        DomainModel.Occupant GetOccupantById(Guid id);
        void SaveRefreshToken(Guid occupantId, DomainModel.RefreshToken refreshToken);
        DomainModel.RefreshToken GetRefreshTokenByOccupantDetailsId(Guid occupantDetailsId);
        void RemoveOldRefreshTokens(Guid occupantId);
        DomainModel.RefreshToken GetChildToken(DomainModel.RefreshToken refreshToken);
        void ReplaceRefreshToken(DomainModel.RefreshToken refreshToken);
        DomainModel.OccupantDetails GetOccupantDetailsByOccupantId(Guid occupantId);
        DomainModel.OccupantRefreshToken GetOccupantDetailsByRefreshToken(string token);
        void RevokeRefreshToken(DomainModel.RefreshToken refreshToken);
        Guid RegisterBuilding(RegisterArgument args);
        RegisterLinkDetails GetRegisterLinkDetails(Guid occupantId);
        void RegisterOccupant(RegisterOccupantArgs args);
    }
}
