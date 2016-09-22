using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class CheckOut
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("box_id")]
        public string Box_id { get; set; }
    }
}