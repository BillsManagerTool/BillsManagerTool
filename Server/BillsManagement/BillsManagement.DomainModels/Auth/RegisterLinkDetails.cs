namespace BillsManagement.DomainModel.Auth
{
    public class RegisterLinkDetails
    {
        public string HousekeeperEmail { get; set; }
        public int BuildingId { get; set; }
        public string BuildingAddress { get; set; }
        public int EntranceId { get; set; }
        public string EntranceNumber { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
    }
}
