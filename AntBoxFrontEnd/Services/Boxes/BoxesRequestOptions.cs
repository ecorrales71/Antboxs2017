﻿using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Boxes
{
    public class BoxesRequestOptions
    {
        [JsonProperty("registered_by")]
        public string Registered_by { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("secure_label")]
        public string Secure_label { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("secure")]
        public decimal Secure { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("activation_date")]
        public string Activation_date { get; set; }

        [JsonProperty("slu")]
        public string Slu{ get; set; }

    }
}