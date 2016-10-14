using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Address
{
    public class AddressValidateRequest
    {
        [JsonProperty("external_number")]
        public string External_number { get; set; }
       
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

    }
}