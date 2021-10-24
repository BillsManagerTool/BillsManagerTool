namespace BillsManagement.Data.Repositories
{
    using AutoMapper;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Contracts.Args;
    using BillsManagement.Data.Models;
    using BillsManagement.DomainModel.Auth;
    using System;
    using System.Linq;

    public class AuthRepository : BaseRepository<Occupant>, IAuthRepository
    {
        public AuthRepository(BillsManager_DevContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public DomainModel.Occupant GetOccupantById(Guid id)
        {
            var occupantEntity = this._context.Occupants.Find(id);

            DomainModel.Occupant occupant = this._mapper.Map<Occupant, DomainModel.Occupant>(occupantEntity);

            return occupant;
        }

        public DomainModel.RefreshToken GetRefreshTokenByOccupantDetailsId(Guid occupantDetailsId)
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

        public DomainModel.OccupantDetails GetOccupantDetailsByOccupantId(Guid occupantId)
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

        public void RegisterHousekeeper(RegisterArgument args)
        {
            var costCenterCode = $"CC-{args.ApartmentNumber}";

            OccupantDetail occupantDetail = new OccupantDetail();
            occupantDetail.OccupantDetailsId = Guid.NewGuid();
            occupantDetail.Email = args.Email;
            occupantDetail.Password = args.Password;

            Occupant occupant = new Occupant();
            occupant.OccupantId = Guid.NewGuid();
            occupant.OccupantDetailsId = occupantDetail.OccupantDetailsId;
            occupant.PeriodStart = DateTime.Now;

            Entrance entrance = new Entrance();
            entrance.EntranceId = Guid.NewGuid();
            entrance.BuildingId = args.BuildingId;
            entrance.EntranceNumber = args.EntranceNumber;

            CostCenter costCenter = new CostCenter();
            costCenter.CostCenterId = Guid.NewGuid();
            costCenter.Code = costCenterCode;
            costCenter.OccupantId = occupant.OccupantId;

            Apartment apartment = new Apartment();
            apartment.ApartmentId = Guid.NewGuid();
            apartment.Number = args.ApartmentNumber;
            apartment.Floor = args.ApartmentFloor;
            apartment.EntranceId = entrance.EntranceId;
            apartment.CostCenterId = costCenter.CostCenterId;

            OccupantToApartment occupantToApartment = new OccupantToApartment();
            occupantToApartment.OccupantToApartmentId = Guid.NewGuid();
            occupantToApartment.OccupantId = occupant.OccupantId;
            occupantToApartment.ApartmentId = apartment.ApartmentId;

            this._context.OccupantDetails.Add(occupantDetail);
            this._context.Occupants.Add(occupant);
            this._context.Entrances.Add(entrance);
            this._context.CostCenters.Add(costCenter);
            this._context.Apartments.Add(apartment);
            this._context.OccupantToApartments.Add(occupantToApartment);
            this._context.SaveChanges();
        }

        public void RemoveOldRefreshTokens(Guid occupantDetailsId)
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

        public void SaveRefreshToken(Guid occupantDetailsId, DomainModel.RefreshToken refreshToken)
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

        public Guid RegisterBuilding(RegisterArgument args)
        {
            Building building = new Building();
            building.BuildingId = Guid.NewGuid();
            building.Address = args.BuildingAddress;
            building.Town = args.Town;
            building.Country = args.Country;

            this._context.Buildings.Add(building);
            this._context.SaveChanges();

            return building.BuildingId;
        }

        public RegisterLinkDetails GetRegisterLinkDetails(Guid occupantId)
        {
            RegisterLinkDetails registerLinkDetails = new RegisterLinkDetails();

            var query = from ota in this._context.OccupantToApartments
                        join o in this._context.Occupants 
                            on occupantId equals o.OccupantId
                        join od in this._context.OccupantDetails
                            on o.OccupantDetailsId equals od.OccupantDetailsId
                        join a in this._context.Apartments
                            on ota.ApartmentId equals a.ApartmentId
                        join e in this._context.Entrances
                            on a.EntranceId equals e.EntranceId
                        join b in this._context.Buildings
                            on e.BuildingId equals b.BuildingId
                        where ota.OccupantId == occupantId
                        select new RegisterLinkDetails()
                        {
                            HousekeeperEmail = od.Email,
                            EntranceId = e.EntranceId,
                            BuildingId = b.BuildingId,
                            BuildingAddress = b.Address
                        };

            return registerLinkDetails = query.FirstOrDefault();
        }

        public void RegisterOccupant(RegisterOccupantArgs args)
        {
            var costCenterCode = $"CC-{args.ApartmentNumber}";

            OccupantDetail occupantDetail = new OccupantDetail();
            occupantDetail.OccupantDetailsId = Guid.NewGuid();
            occupantDetail.Email = args.Email;
            occupantDetail.Password = args.Password;

            Occupant occupant = new Occupant();
            occupant.OccupantId = Guid.NewGuid();
            occupant.OccupantDetailsId = occupantDetail.OccupantDetailsId;
            occupant.PeriodStart = DateTime.Now;

            CostCenter costCenter = new CostCenter();
            costCenter.CostCenterId = Guid.NewGuid();
            costCenter.Code = costCenterCode;
            costCenter.OccupantId = occupant.OccupantId;

            Apartment apartment = new Apartment();
            apartment.ApartmentId = Guid.NewGuid();
            apartment.Number = args.ApartmentNumber;
            apartment.Floor = args.ApartmentFloor;
            apartment.EntranceId = args.EntranceId;
            apartment.CostCenterId = costCenter.CostCenterId;

            OccupantToApartment occupantToApartment = new OccupantToApartment();
            occupantToApartment.OccupantToApartmentId = Guid.NewGuid();
            occupantToApartment.OccupantId = occupant.OccupantId;
            occupantToApartment.ApartmentId = apartment.ApartmentId;

            this._context.OccupantDetails.Add(occupantDetail);
            this._context.Occupants.Add(occupant);
            this._context.CostCenters.Add(costCenter);
            this._context.Apartments.Add(apartment);
            this._context.OccupantToApartments.Add(occupantToApartment);
            this._context.SaveChanges();
        }
    }
}
