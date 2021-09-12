namespace BillsManagement.Core.Controllers
{
    using BillsManagement.DataContracts.Auth;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;

    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
            : base(service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route("register")]
        // POST: /api/auth/register
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
        [Route("authenticate")]
        // POST: /api/auth/login
        public ActionResult<LoginResponse> Authenticate(LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();

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
