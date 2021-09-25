namespace BillsManagement.Data.Repositories
{
    using AutoMapper;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Models;
    using System.Linq;

    public class OccupantRepository : BaseRepository<DomainModel.Occupant>, IOccupantRepository
    {

        public OccupantRepository(BillsManager_DevContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public DomainModel.DetailedOccupant GetOccupantDetailsById(int id)
        {
            var occupant = this._context.Occupants.SingleOrDefault(x => x.OccupantId == id);
            var occupantDetailsEntity = this._context.OccupantDetails.SingleOrDefault(x => x.OccupantDetailsId == occupant.OccupantDetailsId);

            var detailedOccupant = this._mapper.Map<OccupantDetail, DomainModel.DetailedOccupant>(occupantDetailsEntity);
            detailedOccupant.OccupantId = occupant.OccupantId;

            return detailedOccupant;
        }
    }
}
