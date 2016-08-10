using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class Charge
    {
        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

    }
}