using System;

namespace BillsManagement.DomainModel.Auth
{
    public class RegisterLinkDetails
    {
        public string HousekeeperEmail { get; set; }
        public Guid BuildingId { get; set; }
        public string BuildingAddress { get; set; }
        public Guid EntranceId { get; set; }
        public string EntranceNumber { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
    }
}
