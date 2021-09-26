namespace BillsManagement.Data.Contracts.Args
{
    public class RegisterArgument
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int TownId { get; set; }
        public int CountryId { get; set; }
        public string BuildingAddress { get; set; }
        public string EntranceNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public int ApartmentFloor { get; set; }
    }
}
