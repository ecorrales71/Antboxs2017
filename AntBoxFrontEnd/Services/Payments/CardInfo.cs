using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class CardInfo
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonProperty("brand")]
        public string Brand { get; set; }


        [JsonProperty("address")]
        public string Address { get; set; }


        [JsonProperty("card_number")]
        public string Card_number { get; set; }


        [JsonProperty("holder_name")]
        public string Holder_name { get; set; }


        [JsonProperty("expiration_year")]
        public int Expiration_year { get; set; }


        [JsonProperty("expiration_month")]
        public int expiration_month { get; set; }

        [JsonProperty("allows_charges")]
        public bool Allows_charges { get; set; }

        [JsonProperty("allows_payouts")]
        public bool Allows_payouts { get; set; }


        [JsonProperty("creation_date")]
        public DateTime Creation_date { get; set; }


        [JsonProperty("bank_name")]
        public string Bank_name { get; set; }


        [JsonProperty("bank_code")]
        public string Bank_code { get; set; }
    }
}