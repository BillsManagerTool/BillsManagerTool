using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class OccupantToApartment
    {
        public Guid OccupantToApartmentId { get; set; }
        public Guid OccupantId { get; set; }
        public Guid ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }
        public virtual Occupant Occupant { get; set; }
    }
}
