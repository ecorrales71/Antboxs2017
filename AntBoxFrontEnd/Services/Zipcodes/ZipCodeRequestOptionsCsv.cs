using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipCodeRequestOptionsCsv
    {
        [JsonProperty("zipcode")]
        public string zipcode { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("neighborhood")]
        public string neighborhood { get; set; }

        [JsonProperty("delegation")]
        public string delegation { get; set; }

        [JsonProperty("latitude")]
        public string latitude { get; set; }

        [JsonProperty("longitude")]
        public string longitude { get; set; }

    }
}
