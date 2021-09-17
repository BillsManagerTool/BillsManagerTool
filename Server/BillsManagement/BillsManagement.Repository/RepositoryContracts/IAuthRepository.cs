namespace BillsManagement.Data.Contracts
{
    using BillsManagement.DataContracts.Args;

    public interface IAuthRepository : IBaseRepository<DomainModel.Occupant>
    {
        void Register(RegisterArgument args);

        bool IsExistingOccupant(string email);

        DomainModel.OccupantDetails GetOccupantDetails(string email);

        DomainModel.Occupant GetOccupantById(int id);

        void SaveRefreshToken(int occupantId, DomainModel.RefreshToken refreshToken);
    }
}
