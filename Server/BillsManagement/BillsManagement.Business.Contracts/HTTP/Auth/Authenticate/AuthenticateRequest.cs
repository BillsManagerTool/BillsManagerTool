namespace BillsManagement.Business.Contracts.HTTP
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
