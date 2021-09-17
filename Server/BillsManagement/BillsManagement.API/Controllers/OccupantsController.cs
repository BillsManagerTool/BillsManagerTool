namespace BillsManagement.API.Controllers
{
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route(Endpoint.BaseOccupantRoute)]
    public class OccupantsController : Controller
    {
        private readonly IOccupantService _service;

        public OccupantsController(IOccupantService service)
        {
            this._service = service;
        }

        [HttpGet]
        [Route(Endpoint.GetOccupant)]
        [Authorize(AuthenticationPolicy.Housekeeper)]
        public ActionResult<DomainModel.DetailedOccupant> GetOccupant()
        {
            try
            {
                var id = HttpContext.Items["UserId"];
                var occupantDetails = this._service.GetOccupantDetailsById(int.Parse(id.ToString()));
                return occupantDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
