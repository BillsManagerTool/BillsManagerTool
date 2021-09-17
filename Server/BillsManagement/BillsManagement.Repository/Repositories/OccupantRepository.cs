//namespace BillsManagement.Repository.Repositories
//{
//    using AutoMapper;
//    using BillsManagement.DAL.Models;
//    using BillsManagement.Repository.RepositoryContracts;
//    using System.Linq;

//    public class OccupantRepository : BaseRepository<OccupantDetail>, IOccupantRepository
//    {

//        public OccupantRepository(BillsManager_DevContext dbContext, IMapper mapper)
//            : base(dbContext, mapper)
//        {
//        }

//        public DomainModel.DetailedOccupant GetOccupantDetailsById(int id)
//        {
//            var occupant = this._dbContext.Occupants.SingleOrDefault(x => x.OccupantId == id);
//            var occupantDetailsEntity = this._dbContext.OccupantDetails.SingleOrDefault(x => x.OccupantDetailsId == occupant.OccupantDetailsId);

//            var detailedOccupant = this._mapper.Map<OccupantDetail, DomainModel.DetailedOccupant>(occupantDetailsEntity);
//            detailedOccupant.OccupantId = occupant.OccupantId;

//            return detailedOccupant;
//        }
//    }
//}
