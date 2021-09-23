namespace BillsManagement.Business.Contracts.HTTP
{
    using System.Text.Json.Serialization;

    public class AuthenticateResponse : BaseResponse
    {
        public AuthenticateResponse()
        {

        }
        public AuthenticateResponse(string email, string token, string refreshToken)
        {
            this.Email = email;
            this.Token = token;
            this.RefreshToken = refreshToken;
        }

        [JsonPropertyName("JWT")]
        public string Token { get; set; }

        public string Email { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
