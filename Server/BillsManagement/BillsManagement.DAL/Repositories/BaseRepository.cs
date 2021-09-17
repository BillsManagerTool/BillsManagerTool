namespace BillsManagement.Data.Repositories
{
    using AutoMapper;
    using BillsManagement.Data.Contracts;
    using BillsManagement.Data.Models;
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

        public DomainModel.Settings GetNotificationSettings(int key)
        {
            var notificationSettingsEntity = this._dbContext.NotificationSettings.FirstOrDefault(x => x.SettingsKey == 1);

            var notificationSettingsModel = this._mapper.Map<NotificationSetting, DomainModel.Settings>(notificationSettingsEntity);

            return notificationSettingsModel;
        }
    }
}
