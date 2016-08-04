using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace AntBoxFrontEnd.Services.Tasks
{
    public class TaskUpdateResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("log")]
        public string Log { get; set; }


    }
}