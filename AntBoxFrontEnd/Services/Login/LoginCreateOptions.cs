using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Login
{
    public class LoginCreateOptions
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }
}