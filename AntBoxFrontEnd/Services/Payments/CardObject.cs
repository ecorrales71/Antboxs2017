using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class CardObject
    {

        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("status")]
        public string Status { get; set; }


        [JsonProperty("state")]
        public string State { get; set; }


        [JsonProperty("info")]
        public CardInfo Info { get; set; }

    }
}