namespace BillsManagement.DomainModel
{
    using System;
    using System.Text.Json.Serialization;
    public class OccupantDetails
    {
        public Guid OccupantDetailsId { get; set; }

        public Guid OccupantId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool? IsHousekeeper { get; set; }

        public bool? IsOwner { get; set; }

        public bool? IsCurrentOccupant { get; set; }

        public string MobileNumber { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
    }
}
