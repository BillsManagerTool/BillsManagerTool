namespace BillsManagement.Repository
{
    using AutoMapper;
    using BillsManagement.DAL.Models;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            #region Charges Translators

            //CreateMap<Charge, DomainModel.Charge>()
            //      .ForMember(destination => destination.ChargeId, options => options.MapFrom(source => source.ChargeId))
            //      .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.UserId))
            //      .ForMember(destination => destination.ChargeTypeId, options => options.MapFrom(source => source.ChargeTypeId))
            //      .ForMember(destination => destination.ChargeDate, options => options.MapFrom(source => source.ChargeDate))
            //      .ForMember(destination => destination.DueAmount, options => options.MapFrom(source => source.DueAmount));

            //CreateMap<DomainModel.Charge, Charge>()
            //      .ForMember(destination => destination.ChargeId, options => options.MapFrom(source => source.ChargeId))
            //      .ForMember(destination => destination.UserId, options => options.MapFrom(source => source.UserId))
            //      .ForMember(destination => destination.ChargeTypeId, options => options.MapFrom(source => source.ChargeTypeId))
            //      .ForMember(destination => destination.ChargeDate, options => options.MapFrom(source => source.ChargeDate))
            //      .ForMember(destination => destination.DueAmount, options => options.MapFrom(source => source.DueAmount));

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
                }));

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
