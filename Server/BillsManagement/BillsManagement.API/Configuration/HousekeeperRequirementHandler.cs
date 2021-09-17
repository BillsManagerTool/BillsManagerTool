namespace BillsManagement.API.Configuration
{
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;

    public class HousekeeperRequirementHandler : AuthorizationHandler<HousekeeperRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HousekeeperRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(claim => claim.Type == "IsHousekeeper");

            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var claimValue = context.User.FindFirst(claim => claim.Type == "IsHousekeeper").Value;
            var isHousekeeper = bool.Parse(context.User.FindFirst(claim => claim.Type == "IsHousekeeper").Value).Equals(true);

            if (claimValue != null && isHousekeeper)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
