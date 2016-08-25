using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services
{
    public class BadRequestResponse : Response
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("log")]
        public string Log { get; set; }

    }
}