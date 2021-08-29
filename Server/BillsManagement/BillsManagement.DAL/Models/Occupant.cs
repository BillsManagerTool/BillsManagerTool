using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Occupant
    {
        public Occupant()
        {
            CostCenters = new HashSet<CostCenter>();
            OccupantToApartments = new HashSet<OccupantToApartment>();
            SecurityTokens = new HashSet<SecurityToken>();
        }

        public int OccupantId { get; set; }
        public int OccupantDetailsId { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime? LeaveDate { get; set; }

        public virtual OccupantDetail OccupantDetails { get; set; }
        public virtual ICollection<CostCenter> CostCenters { get; set; }
        public virtual ICollection<OccupantToApartment> OccupantToApartments { get; set; }
        public virtual ICollection<SecurityToken> SecurityTokens { get; set; }
    }
}
