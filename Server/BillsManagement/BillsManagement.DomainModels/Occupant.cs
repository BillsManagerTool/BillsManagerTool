using System;

namespace BillsManagement.DomainModel
{
    public class Occupant
    {
        public Guid OccupantId { get; set; }
        public OccupantDetails OccupantDetails { get; set; }
    }
}
