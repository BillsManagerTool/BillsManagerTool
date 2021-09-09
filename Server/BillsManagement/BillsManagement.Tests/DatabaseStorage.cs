using System.Collections.Generic;

namespace BillsManagement.Tests
{
    public class DatabaseStorage
    {
        Dictionary<int, DomainModel.Occupant> _occupantsDatabase = new Dictionary<int, DomainModel.Occupant>();

        public Dictionary<int, DomainModel.Occupant> OccupantsDatabase
        {
            get
            {
                return this._occupantsDatabase;
            }
        }
    }
}
