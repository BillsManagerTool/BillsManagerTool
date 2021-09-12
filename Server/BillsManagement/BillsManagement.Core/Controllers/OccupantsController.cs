namespace BillsManagement.Core.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/occupants")]
    public class OccupantsController : Controller
    {
        [HttpGet]
        [Route("occupant")]
        [Authorize("Housekeeper")]
        // POST: /api/auth/register
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
