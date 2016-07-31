using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Entities
{
    public class HovaAuthResponse
    {
        [JsonProperty("token_type")]
        public string Token_type { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public int Expires_in { get; set; }

        [JsonProperty("access_token")]
        public string Access_token { get; set; }
    }
}