using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services
{
    public class MissingResponse : Response
    {
        [JsonProperty("missing")]
        public string Missing { get; set; }
    }
}