using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Entities
{
    public class MissingError
    {
        [JsonProperty("missing")]
        public string Missing { get; set; }
    }
}