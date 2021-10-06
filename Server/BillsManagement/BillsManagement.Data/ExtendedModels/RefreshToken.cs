namespace BillsManagement.Data.Models
{
    using System;

    public partial class RefreshToken
    {
        public bool IsExpired => DateTime.UtcNow >= this.Expires;
        public bool IsRevoked => this.Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
