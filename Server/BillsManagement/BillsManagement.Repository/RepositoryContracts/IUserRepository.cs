namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System;

    public interface IUserRepository : IBaseRepository<Occupant>
    {
        void Register(string email, string password);

        bool IsExistingOccupant(string email);

        DomainModel.OccupantDetails GetOccupantDetails(string email);

        //DomainModel.SecurityToken GetAuthorizationByUserId(Guid userId);

        Guid GetOccupantInformation(string email);

        void UpdateToken(DomainModel.SecurityToken token);
    }
}
