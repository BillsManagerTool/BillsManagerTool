namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        public DomainModel.Settings GetNotificationSettings(int key);
    }
}
