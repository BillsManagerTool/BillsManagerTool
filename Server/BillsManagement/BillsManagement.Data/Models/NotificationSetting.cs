using System;
using System.Collections.Generic;

#nullable disable

namespace BillsManagement.Data.Models
{
    public partial class NotificationSetting
    {
        public int SettingsKey { get; set; }
        public string BusinessEmail { get; set; }
        public string Password { get; set; }
    }
}
