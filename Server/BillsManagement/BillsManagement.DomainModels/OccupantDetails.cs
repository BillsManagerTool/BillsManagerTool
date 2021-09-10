﻿namespace BillsManagement.DomainModel
{
    public class OccupantDetails
    {
        public int OccupantDetailsId { get; set; }

        public int OccupantId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool? IsHousekeeper { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }
    }
}
