using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class PaymentRequestOptions
    {

        [JsonProperty("token")]
        public string Token { get; set; }


        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }


        [JsonProperty("device_id")]
        public string Device_id { get; set; }


        [JsonProperty("state")]
        public string State { get; set; }

    }
}