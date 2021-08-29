namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IAuthorizationRepository : IBaseRepository<DomainModel.Occupant>
    {
        void SaveSecurityToken(DomainModel.SecurityToken securityToken);

        void UpdateToken(DomainModel.SecurityToken securityToken);
    }
}
