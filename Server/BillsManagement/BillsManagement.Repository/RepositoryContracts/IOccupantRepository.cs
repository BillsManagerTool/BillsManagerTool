namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;

    public interface IOccupantRepository : IBaseRepository<OccupantDetail>
    {
        OccupantDetail GetOccupantDetailsById(int id);
    }
}
