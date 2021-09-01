namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;
    using System.Collections.Generic;

    public interface IChargesRepository : IBaseRepository<Charge>
    {
        List<DomainModel.Charge> GetCharges();
    }
}
