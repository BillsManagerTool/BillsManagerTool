﻿namespace BillsManagement.Core.Controllers
{
    using BillsManagement.DomainModel;
    using BillsManagement.Services.ServiceContracts;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;

    [Route("rest/user")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
            : base(service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route("register")]
        // POST: /rest/user/register
        public ActionResult<DomainModel.RegisterResponse> Register(RegisterRequest request)
        {
            try
            {
                RegisterResponse response = new RegisterResponse();
                response = this._service.Register(request);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("login")]
        // POST: /rest/user/login
        public ActionResult<DomainModel.LoginResponse> Login(LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                response = this._service.Login(request);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
