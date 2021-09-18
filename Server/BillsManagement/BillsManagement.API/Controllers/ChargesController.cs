namespace BillsManagement.API.Controllers
{
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Charges controller.
    /// </summary>
    [Route("rest/charges")]
    [ApiController]
    public class ChargesController : BaseController
    {
        private readonly IChargesService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChargesController"/> class.
        /// </summary>
        public ChargesController(IChargesService service, IAuthService userService)
        {
            this._service = service;
        }
    }
}
