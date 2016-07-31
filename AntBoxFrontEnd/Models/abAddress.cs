using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Models
{
    public class abAddress
    {

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("axternal_number")]
        public string External_number { get; set; }

        [JsonProperty("internal_number")]
        public string Internal_number { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }
       // public string Notes { get; set; }


    }
}