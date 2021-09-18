namespace BillsManagement.API.Controllers
{
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;

    [ApiController]
    [Route(Endpoint.BaseAuthRoute)]
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route(Endpoint.Register)]
        public ActionResult<RegisterResponse> Register(RegisterRequest request)
        {
            try
            {
                RegisterResponse response = new RegisterResponse();
                response = this._service.Register(request);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route(Endpoint.Authenticate)]
        public ActionResult<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            try
            {
                AuthenticateResponse response = new AuthenticateResponse();

                var ipAddress = this.GetIpAddress();

                response = this._service.Authenticate(request, ipAddress);
                this.SetTokenCookie(response.RefreshToken);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
