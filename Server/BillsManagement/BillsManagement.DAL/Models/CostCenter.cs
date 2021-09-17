using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class CostCenter
    {
        public CostCenter()
        {
            Apartments = new HashSet<Apartment>();
            Charges = new HashSet<Charge>();
        }

        public int CostCenterId { get; set; }
        public int OccupantId { get; set; }
        public string Code { get; set; }
        public decimal? TotalDueAmount { get; set; }

        public virtual Occupant Occupant { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Charge> Charges { get; set; }
    }
}
