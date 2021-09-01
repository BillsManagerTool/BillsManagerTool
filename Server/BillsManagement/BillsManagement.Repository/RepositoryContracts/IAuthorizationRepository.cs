namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;

    public interface IAuthorizationRepository : IBaseRepository<Occupant>
    {
        void SaveSecurityToken(DomainModel.SecurityToken securityToken);

        void UpdateToken(DomainModel.SecurityToken securityToken);
    }
}
