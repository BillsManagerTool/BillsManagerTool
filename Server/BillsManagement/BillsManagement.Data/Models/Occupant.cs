using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Occupant
    {
        public Occupant()
        {
            CostCenters = new HashSet<CostCenter>();
            OccupantToApartments = new HashSet<OccupantToApartment>();
        }

        public Guid OccupantId { get; set; }
        public Guid OccupantDetailsId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime? LeaveDate { get; set; }

        public virtual OccupantDetail OccupantDetails { get; set; }
        public virtual ICollection<CostCenter> CostCenters { get; set; }
        public virtual ICollection<OccupantToApartment> OccupantToApartments { get; set; }
    }
}
