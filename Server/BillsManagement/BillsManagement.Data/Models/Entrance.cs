using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Entrance
    {
        public Entrance()
        {
            Apartments = new HashSet<Apartment>();
        }

        public Guid EntranceId { get; set; }
        public Guid BuildingId { get; set; }
        public string EntranceNumber { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
