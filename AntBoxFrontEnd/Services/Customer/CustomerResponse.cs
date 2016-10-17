using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

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

        [JsonProperty("zipcode")]
        int Zipcode { get; set; }

        [JsonProperty("street")]
        string Street { get; set; }

        [JsonProperty("external_number")]
        int External_number { get; set; }

        [JsonProperty("internal_number")]
        int Internal_number { get; set; }

        [JsonProperty("neighborhood")]
        string Neighborhood { get; set; }

        [JsonProperty("state")]
        string State { get; set; }

        [JsonProperty("delegation")]
        string Delegation { get; set; }

    }
}