using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Data.Contracts.Args
{
    public class RegisterOccupantArgs
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ApartmentNumber { get; set; }

        public int ApartmentFloor { get; set; }

        public int BuildingId { get; set; }

        public int EntranceId { get; set; }
    }
}
