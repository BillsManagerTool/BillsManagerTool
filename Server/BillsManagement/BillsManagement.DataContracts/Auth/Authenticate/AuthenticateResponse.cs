namespace BillsManagement.DataContracts.Auth
{
    using System.Text.Json.Serialization;

    public class AuthenticateResponse : BaseResponse
    {
        [JsonPropertyName("JWT")]
        public string Token { get; set; }

        public string Email { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
