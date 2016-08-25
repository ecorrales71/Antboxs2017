using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Worker
{
    public class Vehicle
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("licensePlate")]
        public string LicensePlate { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }
}