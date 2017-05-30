using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Boxes;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CountingCustomersResponse
    {

        [JsonProperty("customers")]
        public int Customers { get; set; }
        
    }
}