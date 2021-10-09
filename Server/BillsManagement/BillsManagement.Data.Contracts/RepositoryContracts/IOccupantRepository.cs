using System;

namespace BillsManagement.Data.Contracts
{
    public interface IOccupantRepository : IBaseRepository<DomainModel.Occupant>
    {
        DomainModel.DetailedOccupant GetOccupantDetailsById(Guid id);
    }
}
