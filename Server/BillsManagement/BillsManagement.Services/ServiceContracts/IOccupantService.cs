namespace BillsManagement.Services.ServiceContracts
{
    public interface IOccupantService
    {
        DomainModel.DetailedOccupant GetOccupantDetailsById(int id);
    }
}
