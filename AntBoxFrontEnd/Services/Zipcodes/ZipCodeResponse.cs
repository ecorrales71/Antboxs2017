using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipCodeResponse
    {

        [JsonProperty("longitude")]
        decimal Logitude { get; set; }

        [JsonProperty("latitude")]
        decimal Latitude { get; set; }

        [JsonProperty("neighborhood")]
        string Neighborhood { get; set; }

        [JsonProperty("zipcode")]
        string Zipcode { get; set; }

        [JsonProperty("delegation")]
        string Delegation { get; set; }

        [JsonProperty("state")]
        string State { get; set; }


    }
}