using AutoMapper;
using BillsManagement.Data.Contracts;
using BillsManagement.DomainModel;
using BillsManagement.Services.ServiceContracts;

namespace BillsManagement.Services.Services.OccupantsService
{
    public class OccupantsService : IOccupantService
    {
        private readonly IOccupantRepository _occupantRepository;

        public OccupantsService(IOccupantRepository occupantRepository)
        {
            this._occupantRepository = occupantRepository;
        }

        public DetailedOccupant GetOccupantDetailsById(int id)
        {
            var detailedOccupant = this._occupantRepository.GetOccupantDetailsById(id);
            return detailedOccupant;
        }
    }
}
