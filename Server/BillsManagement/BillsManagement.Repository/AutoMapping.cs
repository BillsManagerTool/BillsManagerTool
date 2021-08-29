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

            CreateMap<SecurityToken, DomainModel.SecurityToken>()
                .ForMember(destination => destination.OccupantId, options => options.MapFrom(source => source.OccupantId))
                .ForMember(destination => destination.Token, options => options.MapFrom(source => source.Token))
                .ForMember(destination => destination.IsExpired, options => options.MapFrom(source => source.IsExpired))
                .ForMember(destination => destination.ExpirationDate, options => options.MapFrom(source => source.ExpirationDate))
                .ForMember(destination => destination.CreationDate, options => options.MapFrom(source => source.CreationDate))
                .ForMember(destination => destination.Secret, options => options.MapFrom(source => source.Secret));


            CreateMap<DomainModel.SecurityToken, SecurityToken>()
                .ForMember(destination => destination.OccupantId, options => options.MapFrom(source => source.OccupantId))
                .ForMember(destination => destination.Token, options => options.MapFrom(source => source.Token))
                .ForMember(destination => destination.IsExpired, options => options.MapFrom(source => source.IsExpired))
                .ForMember(destination => destination.ExpirationDate, options => options.MapFrom(source => source.ExpirationDate))
                .ForMember(destination => destination.CreationDate, options => options.MapFrom(source => source.CreationDate))
                .ForMember(destination => destination.Secret, options => options.MapFrom(source => source.Secret));

            #endregion
        }
    }
}
