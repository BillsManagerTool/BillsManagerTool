using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Data.Models
{
    public partial class RefreshToken
    {
        public bool IsExpired => DateTime.UtcNow >= this.Expires;
        public bool IsRevoked => this.Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;
    }
}
