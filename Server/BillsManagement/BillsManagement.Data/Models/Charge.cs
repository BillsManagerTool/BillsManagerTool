using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Charge
    {
        public Guid ChargeId { get; set; }
        public Guid CostCenterId { get; set; }
        public Guid CostTypeId { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsPaid { get; set; }

        public virtual CostCenter CostCenter { get; set; }
        public virtual CostType CostType { get; set; }
    }
}
