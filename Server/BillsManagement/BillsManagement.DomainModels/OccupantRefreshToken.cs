using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.DomainModel
{
    public class OccupantRefreshToken
    {
        public DomainModel.OccupantDetails OccupantDetails { get; set; }
        public DomainModel.RefreshToken RefreshToken { get; set; }
    }
}
