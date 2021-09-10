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
                //RegisterResponse response = new RegisterResponse();
                //response = this._service.Register(request);
                //response.StatusCode = HttpStatusCode.OK;
                return "Occupant";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
