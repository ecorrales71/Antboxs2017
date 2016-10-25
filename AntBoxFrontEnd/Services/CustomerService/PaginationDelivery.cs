using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.CustomerService
{
    public class PaginationDelivery
    {
        public PaginationDelivery()
        {
            Deliveries = new List<DeliveryResponse>(); 
        }
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("deliveries")]
        public List<DeliveryResponse> Deliveries { get; set; }
    }
}