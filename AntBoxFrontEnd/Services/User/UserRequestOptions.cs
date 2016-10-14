using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.User
{
    public class UserRequestOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile_phone")]
        public string Mobile_phone { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("change_password")]
        public bool? Change_password { get; set; }

    }
}