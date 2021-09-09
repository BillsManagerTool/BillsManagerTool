using BillsManagement.DataContracts.Auth;
using System.Collections.Generic;

namespace BillsManagement.Tests.Tests.AuthServiceTests.RegisterTests
{
    public partial class RegisterTests
    {
        public static IEnumerable<object[]> Register_0_Valid_Test_TestCaseParams =>
        new TestCase(
            new RegisterRequestData()
            {
                Request = new RegisterRequest()
                {
                    Email = "testmail@gmail.com",
                    Password = "TestHashedPassword"
                }
            },
            null,
            new RegisterResponse()
        ).Params;
    }
}
