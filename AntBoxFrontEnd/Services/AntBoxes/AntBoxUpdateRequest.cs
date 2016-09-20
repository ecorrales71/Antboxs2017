using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class AntBoxUpdateRequest
    {

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}