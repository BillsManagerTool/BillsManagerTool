namespace BillsManagement.DataContracts.Auth
{
    using System.Text.Json.Serialization;

    public class LoginResponse : BaseResponse
    {
        [JsonPropertyName("JWT")]
        public string Token { get; set; }
    }
}
