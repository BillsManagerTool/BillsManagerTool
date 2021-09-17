namespace BillsManagement.Repository
{
    using AutoMapper;
    using BillsManagement.DAL.Models;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            #region Occupants Translators

            CreateMap<OccupantDetail, DomainModel.OccupantDetails>()
                  .ForMember(destination => destination.OccupantDetailsId, options => options.MapFrom(source => source.OccupantDetailsId))
                  .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
                  .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
                  .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                  .ForMember(destination => destination.MobileNumber, options => options.MapFrom(source => source.MobileNumber))
                  .ForMember(destination => destination.IsHousekeeper, options => options.MapFrom(source => source.IsHousekeeper))
                  .ForMember(destination => destination.IsOwner, options => options.MapFrom(source => source.IsOwner))
                  .ForMember(destination => destination.IsCurrentOccupant, options => options.MapFrom(source => source.IsCurrentOccupant));

            CreateMap<OccupantDetail, DomainModel.DetailedOccupant>()
                  .ForMember(destination => destination.OccupantDetailsId, options => options.MapFrom(source => source.OccupantDetailsId))
                  .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
                  .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
                  .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
                  .ForMember(destination => destination.MobileNumber, options => options.MapFrom(source => source.MobileNumber))
                  .ForMember(destination => destination.IsHousekeeper, options => options.MapFrom(source => source.IsHousekeeper))
                  .ForMember(destination => destination.IsOwner, options => options.MapFrom(source => source.IsOwner))
                  .ForMember(destination => destination.IsCurrentOccupant, options => options.MapFrom(source => source.IsCurrentOccupant)).ReverseMap();

            #endregion

            #region Authentication Translators

            CreateMap<Occupant, DomainModel.Occupant>()
                .ForMember(destination => destination.OccupantId, options => options.MapFrom(source => source.OccupantId))
                .ForMember(destination => destination.OccupantDetails, options => options.MapFrom(source => new OccupantDetail
                {
                    FirstName = source.OccupantDetails.FirstName,
                    LastName = source.OccupantDetails.LastName,
                    MobileNumber = source.OccupantDetails.MobileNumber,
                    Email = source.OccupantDetails.Email,
                    Password = source.OccupantDetails.Password
                })).ReverseMap();

            #endregion

            #region Settings Translators

            CreateMap<NotificationSetting, DomainModel.Settings>()
                .ForMember(destination => destination.BusinessEmail, options => options.MapFrom(source => source.BusinessEmail))
                .ForMember(destination => destination.BusinessEmailPassword, options => options.MapFrom(source => source.Password));

            #endregion

            #region Security Translators

            CreateMap<DomainModel.RefreshToken, RefreshToken>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.Token, options => options.MapFrom(source => source.Token))
                .ForMember(destination => destination.Expires, options => options.MapFrom(source => source.Expires))
                .ForMember(destination => destination.Created, options => options.MapFrom(source => source.Created))
                .ForMember(destination => destination.Revoked, options => options.MapFrom(source => source.Revoked))
                .ForMember(destination => destination.ReasonRevoked, options => options.MapFrom(source => source.ReasonRevoked))
                .ForMember(destination => destination.ReplacedByToken, options => options.MapFrom(source => source.ReplacedByToken))
                .ReverseMap();

            #endregion
        }
    }
}
