namespace BillsManagement.API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;

    /// <summary>
    /// Base controller.
    /// </summary>
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

        /// <summary>
        /// Append refresh token to response cookie
        /// </summary>
        /// <param name="token">Refresh token</param>
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

        /// <summary>
        /// Getting ip address
        /// </summary>
        /// <returns>Ip address</returns>
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
