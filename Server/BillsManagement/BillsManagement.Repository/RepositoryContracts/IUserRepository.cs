namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;

    public interface IUserRepository : IBaseRepository<Occupant>
    {
        void Register(string email, string password);

        bool IsExistingOccupant(string email);

        DomainModel.OccupantDetails GetOccupantDetails(string email);

        //DomainModel.SecurityToken GetAuthorizationByUserId(Guid userId);

        int GetOccupantInformation(string email);

        void UpdateToken(DomainModel.SecurityToken token);
    }
}
