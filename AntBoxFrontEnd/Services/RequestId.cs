using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services
{
    public class RequestId
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}