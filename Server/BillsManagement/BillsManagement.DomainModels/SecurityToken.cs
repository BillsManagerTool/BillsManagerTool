namespace BillsManagement.DomainModel
{
    using System;

    public class SecurityToken
    {
        public int OccupantId { get; set; }

        public string Token { get; set; }

        public bool? IsExpired { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Secret { get; set; }
    }
}
