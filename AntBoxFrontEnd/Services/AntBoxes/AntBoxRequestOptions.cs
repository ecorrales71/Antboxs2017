using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class AntBoxRequestOptions
    {

        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("worker_id")]
        public string Worker_id { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("box_id")]
        public string Box_id { get; set; }

        [JsonProperty("antboxs")]
        public string[] Antboxs { get; set; }


    }
}