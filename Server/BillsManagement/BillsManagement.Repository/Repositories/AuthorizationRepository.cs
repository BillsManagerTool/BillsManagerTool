namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System.Linq;

    public class AuthorizationRepository : BaseRepository<DomainModel.User>, IAuthorizationRepository
    {
        public AuthorizationRepository(BillsManager_DevContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public void SaveSecurityToken(DomainModel.SecurityToken securityToken)
        {
            SecurityToken mappedSecurityToken = this._mapper.Map<DomainModel.SecurityToken, SecurityToken>(securityToken);

            this._dbContext.SecurityTokens.Add(mappedSecurityToken);
            this._dbContext.SaveChanges();
        }

        public void UpdateToken(DomainModel.SecurityToken securityToken)
        {
            var prevSecurityToken = this._dbContext.SecurityTokens
                .FirstOrDefault(st => st.IsExpired == false && st.OccupantId == securityToken.OccupantId);

            if (prevSecurityToken != null)
            {
                prevSecurityToken.IsExpired = true;
            }

            var mappedSecurityToken = this._mapper.Map<DomainModel.SecurityToken, SecurityToken>(securityToken);

            this._dbContext.Add(mappedSecurityToken);
            this._dbContext.SaveChanges();
        }
    }
}
