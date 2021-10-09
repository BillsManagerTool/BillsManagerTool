using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Security
{
    public interface IRegisterLinkUtils
    {
        string GenerateRegisterToken(Guid occupantId, Guid buildingId, Guid entranceId);
        ExtractedRegisterToken ValidateRegisterToken(string token);
    }
}
