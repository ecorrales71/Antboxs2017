using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerResponse
    {
        [JsonProperty("missing")]
        public string Missing { get; set; }

    }
}