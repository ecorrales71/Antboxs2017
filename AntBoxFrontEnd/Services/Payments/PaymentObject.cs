using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class PaymentObject
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }


        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("email")]
        public CardInfo Email { get; set; }

        [JsonProperty("rfc")]
        public CardInfo Rfc { get; set; }

        [JsonProperty("type")]
        public CardInfo Type { get; set; }

        [JsonProperty("folio")]
        public CardInfo Folio { get; set; }

        [JsonProperty("amount")]
        public CardInfo Amount { get; set; }

        [JsonProperty("payment_id")]
        public CardInfo Payment_id  { get; set; }
    }
}