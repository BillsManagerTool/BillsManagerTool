namespace BillsManagement.Data.Contracts
{
    using System.Collections.Generic;

    public interface IChargesRepository : IBaseRepository<DomainModel.Charge>
    {
        List<DomainModel.Charge> GetCharges();
    }
}
