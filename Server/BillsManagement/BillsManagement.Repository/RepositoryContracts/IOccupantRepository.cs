namespace BillsManagement.Repository.RepositoryContracts
{
    using BillsManagement.DAL.Models;

    public interface IOccupantRepository : IBaseRepository<OccupantDetail>
    {
        DomainModel.DetailedOccupant GetOccupantDetailsById(int id);
    }
}
