using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Worker
{
    public class WorkerUpdateOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}