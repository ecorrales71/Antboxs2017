using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Coupon
{
    public class CouponUpdateOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("from")]
        public string From{ get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("created_by")]
        public string Created_by { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

    }
}