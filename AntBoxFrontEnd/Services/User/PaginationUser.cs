using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.User
{
    public class PaginationUser
    {
        public PaginationUser()
        {
            Users = new List<UserResponse>(); 
        }
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("users")]
        public List<UserResponse> Users { get; set; }
    }
}