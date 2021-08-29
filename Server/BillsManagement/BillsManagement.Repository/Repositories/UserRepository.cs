namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.DomainModel;
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Linq;
    using System.Net;

    public class UserRepository : BaseRepository<DAL.Models.Occupant>, IUserRepository
    {
        public UserRepository(BillsManager_DevContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public DomainModel.OccupantDetails GetOccupantDetails(string email)
        {
            var queryDetails = from OccupantDetail od in this._dbContext.OccupantDetails
                               where od.Email == email
                               select new OccupantDetails()
                               {
                                   OccupantDetailsId = od.OccupantDetailsId,
                                   OccupantId = od.Occupants.Where(x => x.OccupantDetailsId == od.OccupantDetailsId).FirstOrDefault().OccupantId,
                                   FirstName = od.FirstName,
                                   LastName = od.LastName,
                                   Email = od.Email,
                                   MobileNumber = od.MobileNumber,
                                   Password = od.Password
                               };

            var occupantDetails = queryDetails.FirstOrDefault();

            if (occupantDetails == null)
            {
                string msg = "Can't read user details.";
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, msg);
            }

            return occupantDetails;
        }

        public Guid GetOccupantInformation(string email)
            => this._dbContext.OccupantDetails
                .FirstOrDefault(x => x.Email == email).OccupantDetailsId;

        public bool IsExistingOccupant(string email)
        {
            OccupantDetail occupant = this._dbContext.OccupantDetails
                .FirstOrDefault(u => u.Email == email);

            if (occupant == null)
            {
                return false;
            }

            return true;
        }

        public void Register(string email, string password)
        {
            if (email == null || email == String.Empty || password == String.Empty)
            {
                string msg = "Invalid request.";
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, msg);
            }

            OccupantDetail occupantDetails = new OccupantDetail()
            {
                OccupantDetailsId = Guid.NewGuid(),
                Email = email,
                Password = password
            };

            DAL.Models.Occupant occupant = new DAL.Models.Occupant()
            {
                OccupantId = Guid.NewGuid(),
                OccupantDetailsId = occupantDetails.OccupantDetailsId,
                PeriodStart = DateTime.Now
            };

            this._dbContext.OccupantDetails.Add(occupantDetails);
            this._dbContext.Occupants.Add(occupant);
            this._dbContext.SaveChanges();
        }

        public void UpdateToken(DomainModel.SecurityToken token)
        {
            var mappedSecurityToken = this._mapper
                .Map<DomainModel.SecurityToken, DAL.Models.SecurityToken>(token);

            this._dbContext.SecurityTokens.Add(mappedSecurityToken);

            foreach (var securityToken in this._dbContext.SecurityTokens
                .Where(x => x.SecurityTokenId != mappedSecurityToken.SecurityTokenId))
            {
                if (securityToken.OccupantId == token.OccupantId)
                {
                    securityToken.IsExpired = true;
                }
            };

            this._dbContext.SaveChanges();
        }
    }
}
