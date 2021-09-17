namespace BillsManagement.API.Configuration
{
    using Microsoft.AspNetCore.Authorization;

    public class HousekeeperRequirement : IAuthorizationRequirement
    {
        public HousekeeperRequirement(bool isHousekeeper)
        {
            IsHousekeeper = isHousekeeper;
        }

        public bool IsHousekeeper { get; set; }
    }
}
