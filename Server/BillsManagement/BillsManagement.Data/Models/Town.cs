using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Town
    {
        public Town()
        {
            Buildings = new HashSet<Building>();
        }

        public int TownId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
    }
}
