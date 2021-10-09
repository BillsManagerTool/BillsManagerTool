using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class CostType
    {
        public CostType()
        {
            Charges = new HashSet<Charge>();
        }

        public Guid CostTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Charge> Charges { get; set; }
    }
}
