namespace BillsManagement.Data.Repositories
{
    using AutoMapper;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Contracts.Args;
    using BillsManagement.Data.Models;
    using System;
    using System.Linq;

    public class AuthRepository : BaseRepository<Occupant>, IAuthRepository
    {
        public AuthRepository(BillsManager_DevContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public DomainModel.Occupant GetOccupantById(int id)
        {
            DomainModel.Occupant occupant = new DomainModel.Occupant();

            using (BillsManager_DevContext context = new BillsManager_DevContext())
            {
                var occupantEntity = context.Occupants.Find(id);
                occupant = this._mapper.Map<Occupant, DomainModel.Occupant>(occupantEntity);
            }

            return occupant;
        }

        public DomainModel.RefreshToken GetRefreshTokenByOccupantDetailsId(int occupantDetailsId)
        {
            DomainModel.RefreshToken refreshTokenModel = new DomainModel.RefreshToken();

            var refreshTokenEntity = this._context.RefreshTokens.SingleOrDefault(x => x.OccupantDetailsId == occupantDetailsId);
            refreshTokenModel = this._mapper.Map<RefreshToken, DomainModel.RefreshToken>(refreshTokenEntity);

            return refreshTokenModel;
        }

        public DomainModel.OccupantDetails GetOccupantDetails(string email)
        {
            DomainModel.OccupantDetails occupantDetails = new DomainModel.OccupantDetails();

            var query = from OccupantDetail od in this._context.OccupantDetails
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

            // TODO: Move to service layer

            //if (occupantDetails == null)
            //{
            //    string msg = "Can't read user details.";
            //    throw new HttpStatusCodeException(HttpStatusCode.NotFound, msg);
            //} 

            return occupantDetails;
        }

        public DomainModel.OccupantDetails GetOccupantDetailsByOccupantId(int occupantId)
        {
            DomainModel.OccupantDetails occupantDetailsModel = new DomainModel.OccupantDetails();

            var occupantEntity = this._context.Occupants.SingleOrDefault(x => x.OccupantId == occupantId);
            var occupantDetailsEntity = this._context.OccupantDetails
                .SingleOrDefault(x => x.OccupantDetailsId == occupantEntity.OccupantDetailsId);
            occupantDetailsModel = this._mapper.Map<OccupantDetail, DomainModel.OccupantDetails>(occupantDetailsEntity);

            return occupantDetailsModel;
        }

        public bool IsExistingOccupant(string email) // TODO: Change name to CheckIfOccupantExistsByEmail
        {
            OccupantDetail occupant = new OccupantDetail();

            occupant = this._context.OccupantDetails.FirstOrDefault(o => o.Email == email);

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

            this._context.Occupants.Add(occupant);
            this._context.SaveChanges();
        }

        public void RemoveOldRefreshTokens(int occupantDetailsId)
        {
            // foreach and del
            var refreshTokenEntity = this._context.RefreshTokens.FirstOrDefault(x => x.OccupantDetailsId == occupantDetailsId);
            var refreshTokenModel = this._mapper.Map<RefreshToken, DomainModel.RefreshToken>(refreshTokenEntity);
            if (!refreshTokenModel.IsActive
                && refreshTokenModel.Created.AddMinutes(5) <= DateTime.UtcNow)
            {
                this._context.RefreshTokens.Remove(refreshTokenEntity);
                this._context.SaveChanges();
            }
        }

        public void ReplaceRefreshToken(DomainModel.RefreshToken refreshToken)
        {
                var refreshTokenEntity = this._mapper.Map<DomainModel.RefreshToken, RefreshToken>(refreshToken);
                this._context.RefreshTokens.Add(refreshTokenEntity);
        }

        public void SaveRefreshToken(int occupantDetailsId, DomainModel.RefreshToken refreshToken)
        {
            var occupantDetailsEntity = this._context.OccupantDetails
                .SingleOrDefault(x => x.OccupantDetailsId == occupantDetailsId);
            var refreshTokenEntity = this._mapper.Map<DomainModel.RefreshToken, RefreshToken>(refreshToken);
            occupantDetailsEntity.RefreshTokens.Add(refreshTokenEntity);
            this._context.Update(occupantDetailsEntity);
            this._context.SaveChanges();
        }

        public DomainModel.RefreshToken GetChildToken(DomainModel.RefreshToken refreshToken)
        {
            DomainModel.RefreshToken refreshTokenModel = new DomainModel.RefreshToken();

            var refreshTokenEntity = this._context.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            refreshTokenModel = this._mapper.Map<RefreshToken, DomainModel.RefreshToken>(refreshTokenEntity);

            return refreshTokenModel;
        }

        public DomainModel.OccupantRefreshToken GetOccupantDetailsByRefreshToken(string token)
        {
            DomainModel.OccupantRefreshToken occupantRefreshTokenModel = new DomainModel.OccupantRefreshToken();

            var refreshTokenEntity = this._context.RefreshTokens.SingleOrDefault(x => x.Token == token);
            var occupantDetailsEntity = this._context.OccupantDetails
                .SingleOrDefault(x => x.OccupantDetailsId == refreshTokenEntity.OccupantDetailsId);
            var refreshTokenModel = this._mapper.Map<RefreshToken, DomainModel.RefreshToken>(refreshTokenEntity);
            var occupantDetailsModel = this._mapper.Map<OccupantDetail, DomainModel.OccupantDetails>(occupantDetailsEntity);
            occupantRefreshTokenModel.RefreshToken = refreshTokenModel;
            occupantRefreshTokenModel.OccupantDetails = occupantDetailsModel;

            return occupantRefreshTokenModel;
        }

        public void RevokeRefreshToken(DomainModel.RefreshToken refreshToken)
        {
            var refreshTokenEntity = this._mapper.Map<DomainModel.RefreshToken, RefreshToken>(refreshToken);
            var token = this._context.RefreshTokens.Find(refreshToken.Id);
            token = refreshTokenEntity;
            this._context.SaveChanges();
        }
    }
}
