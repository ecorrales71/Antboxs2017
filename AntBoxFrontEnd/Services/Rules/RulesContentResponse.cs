using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Rules
{
    public class RulesContentResponse : Response
    {
        [JsonProperty("iva")]
        public string Iva { get; set; }

        [JsonProperty("rules")]
        public List<RulesResponse> Rules { get; set; }
    }
}