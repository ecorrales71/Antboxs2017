using Newtonsoft.Json;
using System;

namespace AntBoxFrontEnd.Services.Worker
{
    public class WorkerRequestOption
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("vehicle")]
        public Vehicle Vehicle { get; set; }

        [JsonProperty("capacity")]
        public string Capacity { get; set; }

     
    }
}