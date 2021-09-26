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
            var occupantEntity = this._context.Occupants.Find(id);

            DomainModel.Occupant occupant = this._mapper.Map<Occupant, DomainModel.Occupant>(occupantEntity);

            return occupant;
        }

        public DomainModel.RefreshToken GetRefreshTokenByOccupantDetailsId(int occupantDetailsId)
        {
            DomainModel.RefreshToken refreshTokenModel = new DomainModel.RefreshToken();

            var refreshTokenEntity = this._context.RefreshTokens
                .SingleOrDefault(x => x.OccupantDetailsId == occupantDetailsId);

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

            occupant = this._context.OccupantDetails
                .FirstOrDefault(o => o.Email == email);

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
            foreach (var refreshTokenEntity in this._context.RefreshTokens
                .Where(x => x.OccupantDetailsId == occupantDetailsId))
            {
                if (!refreshTokenEntity.IsActive
                    && refreshTokenEntity.Created.Value.AddMinutes(5) <= DateTime.UtcNow)
                {
                    this._context.RefreshTokens.Remove(refreshTokenEntity);
                }
            }

            this._context.SaveChanges();
        }

        public void ReplaceRefreshToken(DomainModel.RefreshToken refreshToken)
        {
            var refreshTokenEntity = this._mapper.Map<DomainModel.RefreshToken, RefreshToken>(refreshToken);

            this._context.RefreshTokens.Add(refreshTokenEntity);
            this._context.SaveChanges();
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

            var refreshTokenEntity = this._context.RefreshTokens
                .SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);

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
            var refreshTokenEntity = this._context.RefreshTokens.SingleOrDefault(x => x.Id == refreshToken.Id);

            refreshTokenEntity.Revoked = refreshToken.Revoked;
            refreshTokenEntity.RevokedByIp = refreshToken.RevokedByIp;
            refreshTokenEntity.ReasonRevoked = refreshToken.ReasonRevoked;
            refreshTokenEntity.ReplacedByToken = refreshToken.ReplacedByToken;

            this._context.RefreshTokens.Update(refreshTokenEntity);
            this._context.SaveChanges();
        }

        public void RegisterBuilding(RegisterArgument args)
        {
            Entrance entranceEntity = new Entrance()
            {
                EntranceNumber = args.EntranceNumber,
                Building = new Building()
                {
                    Address = args.BuildingAddress,
                    TownId = args.TownId,
                }
            };

            this._context.Entrances.Add(entranceEntity);
            this._context.SaveChanges();
        }
    }
}
