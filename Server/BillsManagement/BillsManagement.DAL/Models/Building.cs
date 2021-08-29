using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Building
    {
        public Building()
        {
            Entrances = new HashSet<Entrance>();
        }

        public int BuildingId { get; set; }
        public int TownId { get; set; }
        public string Address { get; set; }

        public virtual Town Town { get; set; }
        public virtual ICollection<Entrance> Entrances { get; set; }
    }
}
