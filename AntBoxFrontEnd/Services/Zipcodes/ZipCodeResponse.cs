using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipCodeResponse : Response
    {

        [JsonProperty("longitude")]
        public decimal Logitude { get; set; }

        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("creation_date")]
        public string Creation_date { get; set; }


    }
}