using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Charge
    {
        public Guid ChargeId { get; set; }
        public Guid CostCenterId { get; set; }
        public int CostTypeId { get; set; }
        public decimal DueAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsPaid { get; set; }

        public virtual CostCenter CostCenter { get; set; }
        public virtual CostType CostType { get; set; }
    }
}
