using System;

namespace BillsManagement.DomainModel
{
    public class User
    {
        public Guid OccupantId { get; set; }

        public Guid OccupantDetailsId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }
    }
}
