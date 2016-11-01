using System;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.BillingAddress
{
    public class BillingAddressRequestOptions
    {
        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("bussiness_name")]
        public string Bussiness_name { get; set; }

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        [JsonProperty("references")]
        public string References { get; set; }
    }
}