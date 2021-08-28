﻿namespace BillsManagement.DomainModel
{
    using System;
    using System.Text.Json.Serialization;

    public class Charge
    {
        [JsonPropertyName("ChargeId")]
        public Guid ChargeId { get; set; }

        [JsonPropertyName("UserId")]
        public Guid? UserId { get; set; }

        [JsonPropertyName("ChargeTypeId")]
        public Guid? ChargeTypeId { get; set; }

        [JsonPropertyName("DueAmount")]
        public decimal DueAmount { get; set; }

        [JsonPropertyName("ChargeDate")]
        public DateTime ChargeDate { get; set; }
    }
}
