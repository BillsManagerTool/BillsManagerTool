﻿namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Linq;
    using System.Net;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BillsManagementContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public DomainModel.User GetUserDetails(string email)
        {
            var user = this._dbContext.Users
                .FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                string msg = "Can't read user details.";
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, msg);
            }

            var mappedAuth = this._mapper
                .Map<User, DomainModel.User>(user);

            return mappedAuth;
        }

        public Guid GetUserInformation(string email)
            => this._dbContext.Users
                .FirstOrDefault(x => x.Email == email).UserId;

        public bool IsExistingUser(string email)
        {
            User user = this._dbContext.Users
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public DomainModel.Registration Register(string email, string password, out DomainModel.Settings settings)
        {
            if (email == null || email == String.Empty || password == String.Empty)
            {
                string msg = "Invalid request.";
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, msg);
            }

            User user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = email,
                Password = password
            };

            var registration = this._mapper
                .Map<User, DomainModel.Registration>(user);

            this._dbContext.Users.Add(user);
            this._dbContext.SaveChanges();

            settings = this.GetNotificationSettings(1);

            return registration;
        }

        public void UpdateToken(DomainModel.Authorization token)
        {
            var authorization = this._mapper
                .Map<DomainModel.Authorization, DAL.Models.Authorization>(token);

            this._dbContext.Authorizations.Add(authorization);

            foreach (var securityToken in this._dbContext.Authorizations
                .Where(x => x.SecurityTokenId != authorization.SecurityTokenId))
            {
                if (securityToken.UserId == token.UserId)
                {
                    securityToken.IsExpired = true;
                }
            };

            this._dbContext.SaveChanges();
        }
    }
}
