namespace BillsManagement.API.Controllers
{
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route(Endpoint.BaseOccupantRoute)]
    public class OccupantsController : BaseController
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
                DomainModel.DetailedOccupant detailedOccupant = new DomainModel.DetailedOccupant();
                detailedOccupant = this._service.GetOccupantDetailsById(this.OccupantId);
                return detailedOccupant;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
