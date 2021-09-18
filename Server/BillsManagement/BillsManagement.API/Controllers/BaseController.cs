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
        /// <summary>
        /// Returns occupants id from the current request extracted token
        /// </summary>
        protected int OccupantId 
        { 
            get
            {
                return int.Parse(HttpContext.Items["UserId"].ToString());
            }
        }

        protected Guid GetUserId()
        {
            var userId = this.User.Claims.FirstOrDefault(claimRecord => claimRecord.Type == "UserId").Value;
            return Guid.Parse(userId);
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
