using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Code
{
    public class CodeResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("created_by")]
        public CustomerResponse Created_by { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("statusname")]
        public string Statusname
        {
            get
            {
                if (Status)
                {
                    return "Activo";
                } else {
                    return "Inactivo";
                }
            }
        }

        [JsonProperty("creation_date")]
        public string Creation_date { get; set; }

    }
}
