using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class RefreshToken
    {
        public int Id { get; set; }
        public Guid OccupantDetailsId { get; set; }
        public string Token { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public string ReasonRevoked { get; set; }

        public virtual OccupantDetail OccupantDetails { get; set; }
    }
}
