using BillsManagement.DataContracts.Args;
using BillsManagement.DomainModel;

namespace BillsManagement.Tests.RepositoryMocks
{
    public class AuthRepository //: IAuthRepository
    {
        public bool CheckIfOccupantExistsById(int occupantId)
        {
            throw new System.NotImplementedException();
        }

        public Settings GetNotificationSettings(int key)
        {
            throw new System.NotImplementedException();
        }

        public OccupantDetails GetOccupantDetails(string email)
        {
            throw new System.NotImplementedException();
        }

        public int GetOccupantInformation(string email)
        {
            throw new System.NotImplementedException();
        }

        public SecurityToken GetSecurityTokenByOccupantId(int occupantIdd)
        {
            throw new System.NotImplementedException();
        }

        public bool IsExistingOccupant(string email)
        {
            return true;
        }

        public void Register(RegisterArgument args)
        {
            DatabaseStorage db = new DatabaseStorage();

            DomainModel.Occupant occupant = new DomainModel.Occupant()
            {
                OccupantId = 1,
                OccupantDetails = new DomainModel.OccupantDetails()
                {
                    OccupantId = 1,
                    Email = args.Email,
                    Password = args.Password
                }
            };

            db.OccupantsDatabase.Add(1, occupant);
        }

        public void UpdateToken(SecurityToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
