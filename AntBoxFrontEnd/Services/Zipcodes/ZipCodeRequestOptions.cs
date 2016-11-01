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
        public string zipcode { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("neighborhood")]
        public string neighborhood { get; set; }

        [JsonProperty("delegation")]
        public string delegation { get; set; }

    }
}
