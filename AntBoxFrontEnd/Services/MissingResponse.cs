using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services
{
    public class MissingResponse
    {
        [JsonProperty("missing")]
        public string Missing { get; set; }
    }
}