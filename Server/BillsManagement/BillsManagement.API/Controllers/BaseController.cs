namespace BillsManagement.API.Controllers
{
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Custom.CustomExceptions;
    using BillsManagement.Utility;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;

    public class BaseController : Controller
    {
        private readonly IAuthService _authService;

        public BaseController(IAuthService authService)
        {
            this._authService = authService;
        }

        protected Guid GetUserId()
        {
            var userId = this.User.Claims.FirstOrDefault(claimRecord => claimRecord.Type == "UserId").Value;
            return Guid.Parse(userId);
        }

        protected void Authorize()
        {
            Claim claim = this.User.Claims
                .FirstOrDefault(claimRecord => claimRecord.Type == "UserId");

            if (claim == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, GlobalConstants.UnauthorizedMessage);
            }

            int extractedOccupantId = int.Parse(claim.Value);
        }

        protected void SetTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        protected string GetIpAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
