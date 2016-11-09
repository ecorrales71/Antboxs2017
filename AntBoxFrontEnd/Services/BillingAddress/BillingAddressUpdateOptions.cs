using System;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.BillingAddress
{
    public class BillingAddressUpdateOptions
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("bussiness_name")]
        public string Bussiness_name { get; set; }

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        [JsonProperty("references")]
        public string References { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("external_number")]
        public string External_number { get; set; }

        [JsonProperty("internal_number")]
        public string Internal_number { get; set; }

        [JsonProperty("neighbourhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("billing_email")]
        public string Billing_email { get; set; }
    }
}