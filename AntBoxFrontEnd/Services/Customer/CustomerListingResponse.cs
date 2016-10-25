using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerListingResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        public string Namev
        {
            get
            {
                string cad = "";
                if (!string.IsNullOrEmpty(Name) && Name != "undefined")
                {
                    cad = Name;
                }
                if (!string.IsNullOrEmpty(Lastname) && Lastname != "undefined") {
                    cad = cad + " " + Lastname;
                }
                if (!string.IsNullOrEmpty(Lastname2) && Lastname2 != "undefined")
                {
                    cad = cad + " " + Lastname2;
                }
                return cad;
            }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("member_since")]
        public string Member_since { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }

        [JsonProperty("antboxs")]
        public int Antboxsnumber { get; set; }

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        public string Rfcv
        {
            get
            {
                if (Rfc_id == "undefined")
                {
                    return "";
                }
                else return Rfc_id;
            }
            
        }
    }
}