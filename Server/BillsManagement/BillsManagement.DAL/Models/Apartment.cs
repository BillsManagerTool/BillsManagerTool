using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class Apartment
    {
        public Apartment()
        {
            OccupantToApartments = new HashSet<OccupantToApartment>();
        }

        public Guid ApartmentId { get; set; }
        public Guid EntranceId { get; set; }
        public Guid CostCenterId { get; set; }
        public int Floor { get; set; }
        public string Number { get; set; }

        public virtual CostCenter CostCenter { get; set; }
        public virtual Entrance Entrance { get; set; }
        public virtual ICollection<OccupantToApartment> OccupantToApartments { get; set; }
    }
}
