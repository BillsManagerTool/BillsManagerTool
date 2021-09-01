namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;

    public interface IAuthRepository : IBaseRepository<Occupant>
    {
        void Register(string email, string password);

        bool IsExistingOccupant(string email);

        DomainModel.OccupantDetails GetOccupantDetails(string email);

        int GetOccupantInformation(string email);

        void UpdateToken(DomainModel.SecurityToken token); // Arg token param
    }
}
