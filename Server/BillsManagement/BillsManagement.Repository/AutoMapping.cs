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

            //CreateMap<Occupant, DomainModel.Registration>()
            //    .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
            //    .ForMember(destination => destination.MiddleName, options => options.MapFrom(source => source.MiddleName))
            //    .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
            //    .ForMember(destination => destination.Address, options => options.MapFrom(source => source.Address))
            //    .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
            //    .ForMember(destination => destination.Phone, options => options.MapFrom(source => source.Phone));

            //CreateMap<Occupant, DomainModel.User>()
            //    .ForMember(destination => destination.OccupantId, options => options.MapFrom(source => source.OccupantId))
            //    .ForMember(destination => destination.FirstName, options => options.MapFrom(source => source.FirstName))
            //    .ForMember(destination => destination.MiddleName, options => options.MapFrom(source => source.MiddleName))
            //    .ForMember(destination => destination.LastName, options => options.MapFrom(source => source.LastName))
            //    .ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email))
            //    .ForMember(destination => destination.Phone, options => options.MapFrom(source => source.Phone))
            //    .ForMember(destination => destination.Address, options => options.MapFrom(source => source.Address))
            //    .ForMember(destination => destination.IsAdmin, options => options.MapFrom(source => source.IsAdmin))
            //    .ForMember(destination => destination.Password, options => options.MapFrom(source => source.Password));

            //CreateMap<OccupantDetail, DomainModel.RegisterResponse>()
            //    .ForMember(destination => destination, options => options.MapFrom(source => new OccupantDetail
            //    {
            //        FirstName = source.FirstName,
            //        LastName = source.LastName,
            //        Email = source.Email,
            //        MobileNumber = source.MobileNumber
            //        // Have more props
            //    }));

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
