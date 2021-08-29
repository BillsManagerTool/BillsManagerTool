using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class CostCenter
    {
        public CostCenter()
        {
            Apartments = new HashSet<Apartment>();
            Charges = new HashSet<Charge>();
        }

        public Guid CostCenterId { get; set; }
        public Guid OccupantId { get; set; }
        public string Code { get; set; }
        public decimal? TotalDueAmount { get; set; }

        public virtual Occupant Occupant { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
        public virtual ICollection<Charge> Charges { get; set; }
    }
}
