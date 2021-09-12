namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.DataContracts.Args;
    using BillsManagement.Repository.RepositoryContracts;
    using System;
    using System.Linq;

    public class AuthRepository : BaseRepository<Occupant>, IAuthRepository
    {
        public AuthRepository(BillsManager_DevContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {

        }

        public DomainModel.OccupantDetails GetOccupantDetails(string email)
        {
            DomainModel.OccupantDetails occupantDetails = new DomainModel.OccupantDetails();

            using (BillsManager_DevContext context = new BillsManager_DevContext())
            {
                var query = from OccupantDetail od in context.OccupantDetails
                            where od.Email == email
                            select new DomainModel.OccupantDetails()
                            {
                                OccupantDetailsId = od.OccupantDetailsId,
                                OccupantId = od.Occupants.Where(x => x.OccupantDetailsId == od.OccupantDetailsId)
                                                         .FirstOrDefault().OccupantId,
                                FirstName = od.FirstName,
                                LastName = od.LastName,
                                Email = od.Email,
                                IsHousekeeper = od.IsHousekeeper,
                                MobileNumber = od.MobileNumber,
                                Password = od.Password
                            };

                occupantDetails = query.FirstOrDefault();
            }

            // TODO: Move to service layer

            //if (occupantDetails == null)
            //{
            //    string msg = "Can't read user details.";
            //    throw new HttpStatusCodeException(HttpStatusCode.NotFound, msg);
            //} 

            return occupantDetails;
        }

        public int GetOccupantInformation(string email)
            => this._dbContext.OccupantDetails
                .FirstOrDefault(x => x.Email == email).OccupantDetailsId;

        public bool IsExistingOccupant(string email) // TODO: Change name to CheckIfOccupantExistsByEmail
        {
            OccupantDetail occupant = new OccupantDetail();

            using (BillsManager_DevContext context = new BillsManager_DevContext())
            {
                occupant = context.OccupantDetails.FirstOrDefault(o => o.Email == email);
            }

            bool isExisting = false;

            if (occupant != null)
            {
                isExisting = true;
            }

            return isExisting;
        }

        public void Register(RegisterArgument arg)
        {
            Occupant occupant = new Occupant()
            {
                OccupantDetails = new OccupantDetail()
                {
                    Email = arg.Email,
                    Password = arg.Password
                },
                PeriodStart = DateTime.Now
            };

            using (BillsManager_DevContext context = new BillsManager_DevContext())
            {
                context.Occupants.Add(occupant);
                context.SaveChanges();
            }
        }

        public void SaveRefreshToken(int occupantDetailsId, DomainModel.RefreshToken refreshToken)
        {

            using (BillsManager_DevContext context = new BillsManager_DevContext())
            {
                var occupantDetailsEntity = context.OccupantDetails.SingleOrDefault(x => x.OccupantDetailsId == occupantDetailsId);

                var refreshTokenEntity = this._mapper.Map<DomainModel.RefreshToken, RefreshToken>(refreshToken);

                occupantDetailsEntity.RefreshTokens.Add(refreshTokenEntity); //??
                context.SaveChanges();
            }
        }
    }
}
