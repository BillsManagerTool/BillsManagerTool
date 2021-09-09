namespace BillsManagement.Tests.Tests.AuthServiceTests.RegisterTests
{
    public partial class RegisterTests
    {
        private void Register(RegisterRequestData data)
        {
            DatabaseStorage db = new DatabaseStorage();

            DomainModel.Occupant occupant = new DomainModel.Occupant()
            {
                OccupantId = 1,
                OccupantDetails = new DomainModel.OccupantDetails()
                {
                    OccupantId = 1,
                }
            };

            db.OccupantsDatabase.Add(1, occupant);
        }
    }
}
