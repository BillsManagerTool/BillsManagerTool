namespace BillsManagement.API.Controllers
{
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.HTTP.Auth.Authenticate;
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;

    /// <summary>
    /// Auth controller.
    /// </summary>
    [ApiController]
    [Route(Endpoint.BaseAuthRoute)]
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        public AuthController(IAuthService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Register an occupant
        /// </summary>
        /// <param name="request">Accepts request of <see cref="Business.Contracts.HTTP.RegisterRequest"/></param>
        /// <returns>ActionResult as an instance of <see cref="Business.Contracts.HTTP.RegisterResponse"/> class.</returns>
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

        /// <summary>
        /// Authenticates the occupant.
        /// After successful authentication generates jwt token.
        /// </summary>
        /// <param name="request">Accepts request of <see cref="Business.Contracts.HTTP.AuthenticateRequest"/></param>
        /// <returns>ActionResult as an instance of <see cref="Business.Contracts.HTTP.AuthenticateResponse"/> class.</returns>
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

        [HttpPost]
        [Route("refresh-token")]
        public ActionResult<AuthenticateResponse> RefreshToken()
        {
            try
            {
                AuthenticateResponse response = new AuthenticateResponse();

                var refreshToken = Request.Cookies["refreshToken"];
                var ipAddress = this.GetIpAddress();

                response = this._service.RefreshToken(refreshToken, ipAddress);
                this.SetTokenCookie(response.RefreshToken);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("register-link")]
        public ActionResult<GenerateRegisterLinkResponse> GenerateRegisterLink()
        {
            try
            {
                GenerateRegisterLinkResponse response = new GenerateRegisterLinkResponse();
                response = this._service.GenerateRegisterLink(this.OccupantId);
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
