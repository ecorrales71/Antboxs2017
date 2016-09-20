using System;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Tasks
{
    public class TaskRequestOption
    {

        [JsonProperty("customer_id")]
        public string customer_id { get; set; }

        [JsonProperty("address_id")]
        public string Address_id { get; set; }

        [JsonProperty("from")]        
        public long from { get; set; }

        [JsonProperty("to")]
        public long To { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("service_time")]
        public bool? Service_time { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("worker_id")]
        public string Worker_id { get; set; }



    }
}