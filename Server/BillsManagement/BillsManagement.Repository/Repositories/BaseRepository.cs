namespace BillsManagement.Repository.Repositories
{
    using AutoMapper;
    using BillsManagement.DAL.Models;
    using BillsManagement.Repository.RepositoryContracts;
    using System.Linq;

    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly BillsManager_DevContext _dbContext;
        public readonly IMapper _mapper;

        public BaseRepository(BillsManager_DevContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public bool CheckIfOccupantExistsById(int occupantId)
        {
            bool isExisting = false;

            Occupant occupant = this._dbContext.Occupants.FirstOrDefault(x => x.OccupantId == occupantId);

            if (occupant != null)
            {
                isExisting = true;
            }

            return isExisting;
        }

        public DomainModel.Settings GetNotificationSettings(int key)
        {
            var notificationSettings = this._dbContext.NotificationSettings.FirstOrDefault(x => x.SettingsKey == 1);

            var settings = this._mapper.Map<NotificationSetting, DomainModel.Settings>(notificationSettings);

            return settings;
        }

        //public DomainModel.SecurityToken GetSecurityTokenByOccupantId(int occupantId)
        //{
        //    SecurityToken token = this._dbContext.SecurityTokens
        //        .FirstOrDefault(x => x.IsExpired == false & x.OccupantId == occupantId);

        //    DomainModel.SecurityToken tokenModel = this._mapper.Map<SecurityToken, DomainModel.SecurityToken>(token);

        //    return tokenModel;
        //}
    }
}
