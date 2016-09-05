using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Boxes
{
    public class BoxesResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }       

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("secure")]
        public decimal Secure { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("registered_by")]
        public CustomerResponse Registered_by { get; set; }


    }
}