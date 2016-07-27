using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Entities
{
    public class ServiceError
    {
        [JsonProperty("code")]
        public string ErrorCode { get; set; }

        [JsonProperty("message")]
        public ServiceErrorMessage Message { get; set; }

    }
}