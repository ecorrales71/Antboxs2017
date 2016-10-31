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

        public string Lastname2v
        {
            get
            {
                if (Lastname2 == "undefined")
                {
                    return "";
                }
                else
                {
                    return Lastname2;
                }
            }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("rfc")]
        public string Rfc { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("payment_id")]
        public string Payment_id { get; set; }
    }
}