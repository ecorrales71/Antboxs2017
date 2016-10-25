using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.CustomerService
{
    public class WorkerResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}