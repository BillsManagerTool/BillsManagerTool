using System;

namespace BillsManagement.Repository.RepositoryContracts
{
    public interface IBaseRepository<T> where T : class
    {
        public DomainModel.SecurityToken GetSecurityTokenByOccupantId(Guid occupantIdd);

        public DomainModel.Settings GetNotificationSettings(int key);

        //public CashAccount GetCashAccountByUserId(Guid? userId);

        public bool CheckIfOccupantExistsById(Guid occupantId);
    }
}
