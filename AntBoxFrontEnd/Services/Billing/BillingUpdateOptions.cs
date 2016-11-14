using System;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Billing
{
    public class BillingUpdateOptions
    {
        [JsonProperty("billing_address_id")]
        public string Billing_address_id { get; set; }

        [JsonProperty("payment_id")]
        public string Payment_id { get; set; }
    }
}