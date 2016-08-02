using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Address
{
    public class AddressResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("external_number")]
        public string External_number { get; set; }

        [JsonProperty("internal_number")]
        public string Internal_number { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
        

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        [JsonProperty("references")]
        public string References { get; set; }

        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }
    }
}