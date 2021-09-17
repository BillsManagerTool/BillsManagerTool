using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class OccupantToApartment
    {
        public int OccupantToApartmentId { get; set; }
        public int OccupantId { get; set; }
        public int ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Occupant Occupant { get; set; }
    }
}
