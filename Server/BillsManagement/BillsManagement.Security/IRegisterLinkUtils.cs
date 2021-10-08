using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Security
{
    public interface IRegisterLinkUtils
    {
        string GenerateRegisterToken(int occupantId, int buildingId, int entranceId);
        int? ValidateRegisterToken(string token);
    }
}
