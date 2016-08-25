using AntBoxFrontEnd.Services.Address;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Boxes
{
    public class PaginationBoxesResponse
    {
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("boxes")]
        public List<BoxesResponse> Boxes { get; set; }
    }
}