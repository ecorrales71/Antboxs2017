using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Tasks
{
    public class TaskUpdateOptions
    {

        [JsonProperty("state")]
        public string State { get; set; }

    }
}