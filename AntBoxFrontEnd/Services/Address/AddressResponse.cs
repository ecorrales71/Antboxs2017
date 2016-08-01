using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Address
{
    public class AddressResponse
    {
        [JsonProperty("alias")]
        string Alias { get; set; }

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

        [JsonProperty("customer_id")]
        string Customer_id { get; set; }
    }
}