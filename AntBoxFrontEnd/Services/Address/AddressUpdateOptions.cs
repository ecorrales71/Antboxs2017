using System;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Address
{
    public class AddressUpdateOptions
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
    }
}