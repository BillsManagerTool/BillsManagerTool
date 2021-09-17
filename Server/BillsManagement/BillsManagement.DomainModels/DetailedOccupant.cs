using System.Text.Json.Serialization;

namespace BillsManagement.DomainModel
{
    public class DetailedOccupant
    {
        [JsonPropertyName("dtls_id")]
        public int OccupantDetailsId { get; set; }

        [JsonPropertyName("ocpnt_id")]
        public int OccupantId { get; set; }

        [JsonPropertyName("frst_nm")]
        public string FirstName { get; set; }

        [JsonPropertyName("lst_nm")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [JsonPropertyName("is_hskpr")]
        public bool? IsHousekeeper { get; set; }

        [JsonPropertyName("is_ownr")]
        public bool? IsOwner { get; set; }

        [JsonPropertyName("is_crnt_ocpnt")]
        public bool? IsCurrentOccupant { get; set; }

        [JsonPropertyName("mbl_nmbr")]
        public string MobileNumber { get; set; }
    }
}
