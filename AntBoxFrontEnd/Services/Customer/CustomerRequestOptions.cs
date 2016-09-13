using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerRequestOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile_phone")]
        public string Mobile_phone { get; set; }

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        [JsonProperty("status")]
        public Boolean Status { get; set; }

        
        [JsonProperty("username")]
        public string Username { get; set; }

    }
}