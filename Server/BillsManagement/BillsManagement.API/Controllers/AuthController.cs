namespace BillsManagement.API.Controllers
{
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.HTTP.Auth.Authenticate;
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Utility.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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
        /// Register housekeeper, create building and entrance
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
        /// After successful authentication generates jwt token and append refresh token to the response cookie.
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

        /// <summary>
        /// Extracts the refresh token from the cookie. If it is valid, generates and returns new json web token 
        /// and attach the new refresh token to the response cookie. Removes the old refresh token from the database.
        /// If the refresh token from request cookie is invalid, the refresh token is revoked in the database and it can't be used to generate
        /// new json web token anymore.
        /// </summary>
        /// <returns>ActionResult as an instance of <see cref="Business.Contracts.HTTP.AuthenticateResponse"/> class.</returns>
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

        /// <summary>
        /// The method can be accessed only by HK.
        /// It is used to generate a query string with the needed information for occupant register invitations.
        /// </summary>
        /// <returns>ActionResult as an instance of <see cref="SendRegisterInvitationResponse"/> class.</returns>
        [HttpPost]
        [Route("invite-occupants")]
        [Authorize(AuthenticationPolicy.Housekeeper)]
        public ActionResult<SendRegisterInvitationResponse> SendRegisterInvitation(SendRegisterInvitationRequest request)
        {
            try
            {
                SendRegisterInvitationResponse response = new SendRegisterInvitationResponse();
                this._service.SendRegisterInvitation(this.OccupantId, request.Emails);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("register-occupant")]
        public ActionResult<RegisterOccupantResponse> RegisterOccupant(RegisterOccupantRequest request)
        {
            try
            {
                RegisterOccupantResponse response = new RegisterOccupantResponse();
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
