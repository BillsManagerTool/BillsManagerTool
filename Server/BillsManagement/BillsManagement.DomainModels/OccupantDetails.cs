﻿using System.Text.Json.Serialization;

namespace BillsManagement.DomainModel
{
    public class OccupantDetails
    {
        public int OccupantDetailsId { get; set; }

        public int OccupantId { get; set; }

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
