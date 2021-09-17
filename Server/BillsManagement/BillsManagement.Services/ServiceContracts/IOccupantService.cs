using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Services.ServiceContracts
{
    public interface IOccupantService
    {
        DomainModel.DetailedOccupant GetOccupantDetailsById(int id);
    }
}
