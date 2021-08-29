using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Entrance
    {
        public Entrance()
        {
            Apartments = new HashSet<Apartment>();
        }

        public int EntranceId { get; set; }
        public int BuildingId { get; set; }
        public string EntranceNumber { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
