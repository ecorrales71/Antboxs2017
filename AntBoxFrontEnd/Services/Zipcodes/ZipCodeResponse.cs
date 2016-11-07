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
                //return dt.ToString(@"dd\/MM\/yyyy");

                int month = dt.Month;
                string monthname = "";
                switch (month)
                {
                    case 1: monthname = "Ene"; break;
                    case 2: monthname = "Feb"; break;
                    case 3: monthname = "Mar"; break;
                    case 4: monthname = "Abr"; break;
                    case 5: monthname = "May"; break;
                    case 6: monthname = "Jun"; break;
                    case 7: monthname = "Jul"; break;
                    case 8: monthname = "Ago"; break;
                    case 9: monthname = "Sep"; break;
                    case 10: monthname = "Oct"; break;
                    case 11: monthname = "Nov"; break;
                    case 12: monthname = "Dic"; break;

                }

                return dt.ToString(@"dd") + "/" + monthname + "/" + dt.ToString(@"yyyy");
            }
            catch
            {
                return date;
            }
        }


    }
}