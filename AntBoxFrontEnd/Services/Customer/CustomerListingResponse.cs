using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerListingResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("antboxs")]
        public string Antboxs { get; set; }

        [JsonProperty("member_since")]
        public string Member_since { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }

    }
}