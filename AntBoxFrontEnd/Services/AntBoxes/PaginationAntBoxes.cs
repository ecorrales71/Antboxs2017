using AntBoxFrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class PaginationAntBoxes
    {
        public List<AntBoxAddressViewModel> Addresses { get; set; }
        public  PaginationAntBoxes()
        {
            Antboxs = new List<AntBoxResponse>(); 
        }
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("antboxs")]
        public List<AntBoxResponse> Antboxs { get; set; }
    }
}