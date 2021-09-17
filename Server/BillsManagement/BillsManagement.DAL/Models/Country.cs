using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            Towns = new HashSet<Town>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AlphaCode3 { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
