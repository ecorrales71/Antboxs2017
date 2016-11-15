using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Rules
{
    public class RulesResponse
    {
        [JsonProperty("max")]
        public string Max { get; set; }

        [JsonProperty("min")]
        public string Min { get; set; }

        [JsonProperty("discount")]
        public string Discount { get; set; } 
    }
}