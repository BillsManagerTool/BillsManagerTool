using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            OccupantToApartments = new HashSet<OccupantToApartment>();
        }

        public int ApartmentId { get; set; }
        public int EntranceId { get; set; }
        public int CostCenterId { get; set; }
        public int Floor { get; set; }
        public string Number { get; set; }

        public virtual CostCenter CostCenter { get; set; }
        public virtual Entrance Entrance { get; set; }
        public virtual ICollection<OccupantToApartment> OccupantToApartments { get; set; }
    }
}
