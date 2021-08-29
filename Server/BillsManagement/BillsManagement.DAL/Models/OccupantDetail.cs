using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class OccupantDetail
    {
        public OccupantDetail()
        {
            Occupants = new HashSet<Occupant>();
        }

        public Guid OccupantDetailsId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool? IsOwner { get; set; }
        public bool? IsHousekeeper { get; set; }
        public bool? IsCurrentOccupant { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Occupant> Occupants { get; set; }
    }
}
