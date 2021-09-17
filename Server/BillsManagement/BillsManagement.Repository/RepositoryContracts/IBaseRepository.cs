namespace BillsManagement.Data.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        public DomainModel.Settings GetNotificationSettings(int key);
    }
}
