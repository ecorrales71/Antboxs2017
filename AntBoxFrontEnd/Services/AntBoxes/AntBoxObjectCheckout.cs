using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class AntBoxObjectCheckout
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("serial")]
        public string Serial { get; set; }

    }
}