using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Login
{
    public class RestoreCreateOptions
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("new_password")]
        public string Newpassword { get; set; }

    }
}