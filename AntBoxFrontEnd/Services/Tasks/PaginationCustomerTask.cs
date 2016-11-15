using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Tasks
{
    public class PaginationCustomerTask
    {
        public List<AntBoxAddressViewModel> Addresses { get; set; }
        public PaginationCustomerTask()
        {
            Tasks = new List<TaskResponse>(); 
        }
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("tasks")]
        public List<TaskResponse> Tasks { get; set; }
    }
}