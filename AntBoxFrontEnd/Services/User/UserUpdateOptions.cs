using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.User
{
    public class UserUpdateOptions
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("mobile_phone")]
        public string Mobile_phone { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("change_password")]
        public Boolean? Change_password { get; set; }

    }
}