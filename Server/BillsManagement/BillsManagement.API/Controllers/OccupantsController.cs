namespace BillsManagement.API.Controllers
{
    using BillsManagement.Services.ServiceContracts;
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Occupants controller.
    /// </summary>
    [ApiController]
    [Route(Endpoint.BaseOccupantRoute)]
    public class OccupantsController : BaseController
    {
        private readonly IOccupantService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="OccupantsController"/> class.
        /// </summary>
        public OccupantsController(IOccupantService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Get occupant details by extracted from the auth token occupant id.
        /// </summary>
        /// <returns>ActionResult as an instance of <see cref="DomainModel.DetailedOccupant"/> class.</returns>
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
