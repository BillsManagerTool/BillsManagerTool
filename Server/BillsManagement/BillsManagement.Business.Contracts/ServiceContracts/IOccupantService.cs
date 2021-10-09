using System;

namespace BillsManagement.Services.ServiceContracts
{
    public interface IOccupantService
    {
        DomainModel.DetailedOccupant GetOccupantDetailsById(Guid id);
    }
}
