using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.CustomerService
{
    public class PaginationPickup
    {
        public PaginationPickup()
        {
            Pickups = new List<PickupResponse>(); 
        }
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("pickups")]
        public List<PickupResponse> Pickups { get; set; }
    }
}