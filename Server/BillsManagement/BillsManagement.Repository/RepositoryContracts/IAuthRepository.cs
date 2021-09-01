namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using BillsManagement.DataContracts.Args;

    public interface IAuthRepository : IBaseRepository<Occupant>
    {
        void Register(RegisterArgument args);

        bool IsExistingOccupant(string email);

        DomainModel.OccupantDetails GetOccupantDetails(string email);

        int GetOccupantInformation(string email);

        void UpdateToken(DomainModel.SecurityToken token); // Arg token param
    }
}
