using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.User
{
    public class UserResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("system_profile")]
        public string System_profile { get; set; }

        [JsonProperty("mobile_phone")]
        public string Mobile_phone { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastnamev")]
        public string Lastnamev
        {
            get
            {
                if (Lastname == "undefined")
                {
                    return "";
                } else {
                    return Lastname;
                }
            }
        }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("profile")]
        public string Profile { get; set; }

        [JsonProperty("change_password")]
        public bool Change_password { get; set; }
    }
}