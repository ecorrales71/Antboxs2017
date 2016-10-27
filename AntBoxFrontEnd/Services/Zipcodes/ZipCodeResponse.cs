using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipCodeResponse : Response
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("neighborhood")]
        public string Neighborhood { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("delegation")]
        public string Delegation { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("creation_date")]
        public string Creation_date { get; set; }

        public string Date_creation {
            get
            {
                return formatdate(Creation_date, "dd-MM-yyyy HH:mm:ss");
            }
        }

        private string formatdate(string date, string format)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(date, format,
                                   System.Globalization.CultureInfo.InvariantCulture);
                return dt.ToString(@"dd\/MM\/yyyy");
            }
            catch
            {
                return date;
            }
        }


    }
}