using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class PaymentDetailResponse : Response
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("payment_id")]
        public string Payment_id { get; set; }

        [JsonProperty("order_id")]
        public string Order_id { get; set; }

        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

    }
}