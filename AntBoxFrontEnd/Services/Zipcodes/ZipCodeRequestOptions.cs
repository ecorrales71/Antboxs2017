using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipCodeRequestOptions
    {
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

    }
}
