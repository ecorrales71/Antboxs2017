using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Address
{
    public class ValidationAddressResponse : Response
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}