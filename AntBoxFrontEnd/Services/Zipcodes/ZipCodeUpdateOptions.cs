using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipCodeUpdateOptions
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }
    }
}
