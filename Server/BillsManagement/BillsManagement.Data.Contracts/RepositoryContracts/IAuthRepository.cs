namespace BillsManagement.Data.Contracts
{
    using BillsManagement.Data.Contracts.Args;

    public interface IAuthRepository : IBaseRepository<DomainModel.Occupant>
    {
        void Register(RegisterArgument args);

        bool IsExistingOccupant(string email);

        DomainModel.OccupantDetails GetOccupantDetails(string email);

        DomainModel.Occupant GetOccupantById(int id);

        void SaveRefreshToken(int occupantId, DomainModel.RefreshToken refreshToken);

        DomainModel.RefreshToken GetRefreshTokenByOccupantDetailsId(int occupantDetailsId);

        void RemoveOldRefreshTokens(int occupantId);

        DomainModel.RefreshToken GetChildToken(DomainModel.RefreshToken refreshToken);

        void ReplaceRefreshToken(DomainModel.RefreshToken refreshToken);

        DomainModel.OccupantDetails GetOccupantDetailsByOccupantId(int occupantId);

        DomainModel.OccupantRefreshToken GetOccupantDetailsByRefreshToken(string token);

        void RevokeRefreshToken(DomainModel.RefreshToken refreshToken);

        void RegisterBuilding(RegisterArgument args);
    }
}
