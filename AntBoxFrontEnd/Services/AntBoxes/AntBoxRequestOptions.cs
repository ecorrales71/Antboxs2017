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

        [JsonProperty("checkouts")]
        public CheckOut[] Checkouts { get; set; }

        [JsonProperty("antboxs")]
        public List<AntBoxObjectCheckout> Antboxs { get; set; }

        [JsonProperty("coupon_id")]
        public string Coupon_id { get; set; }

        [JsonProperty("address_id")]
        public string Address_id { get; set; }
    }
}