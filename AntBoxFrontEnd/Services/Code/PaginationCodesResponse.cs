using AntBoxFrontEnd.Services.Address;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Code
{
    public class PaginationCodesResponse
    {
        [JsonProperty("pagination_id")]
        public string Pagination_id { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("codes")]
        public List<CodeResponse> Codes { get; set; }
    }
}
