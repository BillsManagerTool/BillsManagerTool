namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        public DomainModel.SecurityToken GetSecurityTokenByOccupantId(int occupantIdd);

        public DomainModel.Settings GetNotificationSettings(int key);

        //public CashAccount GetCashAccountByUserId(Guid? userId);

        public bool CheckIfOccupantExistsById(int occupantId);
    }
}
