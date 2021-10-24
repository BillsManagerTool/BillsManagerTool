using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Building
    {
        public Building()
        {
            Entrances = new HashSet<Entrance>();
        }

        public Guid BuildingId { get; set; }
        //public int TownId { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Entrance> Entrances { get; set; }
    }
}
