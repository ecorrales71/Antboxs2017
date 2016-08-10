using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class ChargeResponse
    {

        [JsonProperty("status")]
        public string Status { get; set; }


        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("amount")]
        public string Amount { get; set; }


        [JsonProperty("creation_date")]
        public string Creation_date { get; set; }

    }
}