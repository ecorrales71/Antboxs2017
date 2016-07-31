using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Entities
{
    public class HovaAuth
    {

        [JsonProperty("grant_type")]
        public String Grant_type { get; set; }

        [JsonProperty("username")]
        public String Username { get; set; }

        [JsonProperty("password")]
        public String Password { get; set; }

        [JsonProperty("scope")]
        public String Scope { get; set; }

    }
}