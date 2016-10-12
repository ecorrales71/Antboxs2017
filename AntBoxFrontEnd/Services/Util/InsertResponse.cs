using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Util
{
    public class InsertResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}