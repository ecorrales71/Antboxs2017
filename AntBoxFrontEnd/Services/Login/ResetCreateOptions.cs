using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Login
{
    public class ResetCreateOptions
    {
        [JsonProperty("email")]
        public string Email { get; set; }

    }
}