using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.BillingAddress
{
    public class BillingAddressResponse : Response
    {
        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("bussiness_name")]
        public string Bussiness_name { get; set; }

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        [JsonProperty("references")]
        public string References { get; set; }

    }
}