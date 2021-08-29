using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.DAL.Models
{
    public partial class SecurityToken
    {
        public Guid SecurityTokenId { get; set; }
        public Guid OccupantId { get; set; }
        public string Token { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Secret { get; set; }

        public virtual Occupant Occupant { get; set; }
    }
}
