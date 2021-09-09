using BillsManagement.DataContracts.Auth;
using BillsManagement.Services.Services.AuthService;
using Xunit;

namespace BillsManagement.Tests.Tests.AuthServiceTests.RegisterTests
{
    public partial class RegisterTests
    {
        [Theory]
        [Trait("AuthService", "Register")]
        [MemberData(nameof(Register_0_Valid_Test_TestCaseParams))]
        public void Register_0_Valid_Test(RegisterRequestData data, object requiredCase, object expectedResult)
        {
            AuthService service = new AuthService();
            this.Register(data);
            RegisterResponse response = service.Register(data.Request);

            Assert.Equal(expectedResult.GetType(), response.GetType());
        }
    }
}
