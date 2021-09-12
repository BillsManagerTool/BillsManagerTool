namespace BillsManagement.Core.Controllers
{
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route(Endpoint.BaseOccupantRoute)]
    public class OccupantsController : Controller
    {
        [HttpGet]
        [Route(Endpoint.GetOccupant)]
        [Authorize(AuthenticationPolicy.Housekeeper)]
        public ActionResult<string> GetOccupant()
        {
            try
            {
                // Thats how we get user id after successfully validating and extracting data from jwt middleware
                var id = HttpContext.Items["UserId"];
                return "Occupant";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
