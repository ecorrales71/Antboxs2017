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
        public string Price { get; set; }

        [JsonProperty("secure")]
        public string Secure { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("registered_by")]
        public CustomerResponse Registered_by { get; set; }




    }
}