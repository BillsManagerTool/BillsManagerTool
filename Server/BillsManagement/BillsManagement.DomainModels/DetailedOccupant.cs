namespace BillsManagement.DomainModel
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Detailed information for the occupant
    /// </summary>
    public class DetailedOccupant
    {
        /// <summary>
        /// Id of the occupant details
        /// </summary>
        [JsonPropertyName("dtls_id")]
        public Guid OccupantDetailsId { get; set; }

        /// <summary>
        /// Id of the occupant
        /// </summary>
        [JsonPropertyName("ocpnt_id")]
        public Guid OccupantId { get; set; }

        /// <summary>
        /// First name of the occupant from details table
        /// </summary>
        [JsonPropertyName("frst_nm")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the occupant from details table
        /// </summary>
        [JsonPropertyName("lst_nm")]
        public string LastName { get; set; }

        /// <summary>
        /// Email of the occupant from details table
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Shows is the user is the housekeeper of the entrance
        /// </summary>
        [JsonPropertyName("is_hskpr")]
        public bool? IsHousekeeper { get; set; }

        /// <summary>
        /// Shows if the user is the owner of the apartment
        /// </summary>
        [JsonPropertyName("is_ownr")]
        public bool? IsOwner { get; set; }

        /// <summary>
        /// Shows if the user is the current occupant of the apartment
        /// </summary>
        [JsonPropertyName("is_crnt_ocpnt")]
        public bool? IsCurrentOccupant { get; set; }


        /// <summary>
        /// Mobile number of the occupant
        /// </summary>
        [JsonPropertyName("mbl_nmbr")]
        public string MobileNumber { get; set; }
    }
}
