namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System.Linq;

    public class OccupantRepository : BaseRepository<OccupantDetail>, IOccupantRepository
    {
        public OccupantRepository(BillsManager_DevContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public OccupantDetail GetOccupantDetailsById(int id)
            => this._dbContext.Occupants.SingleOrDefault(x => x.OccupantId == id).OccupantDetails;
    }
}
