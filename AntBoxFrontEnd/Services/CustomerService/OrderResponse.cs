using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Customer;

namespace AntBoxFrontEnd.Services.CustomerService
{
    public class OrderResponse : Response
    {
        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("antboxs")]
        public int Antboxs { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("requested_at")]
        public string Requested_at { get; set; }

        [JsonProperty("pickup_at")]
        public string Pickup_at { get; set; }

        [JsonProperty("delivery_at")]
        public string Delivery_at { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        public string Namev
        {
            get
            {
                string cad = "";
                if (!string.IsNullOrEmpty(Name) && Name != "undefined")
                {
                    cad = Name;
                }
                if (!string.IsNullOrEmpty(Lastname) && Lastname != "undefined")
                {
                    cad = cad + " " + Lastname;
                }
                if (!string.IsNullOrEmpty(Lastname2) && Lastname2 != "undefined")
                {
                    cad = cad + " " + Lastname2;
                }
                return cad;
            }
        }

        public string Requested_date
        {
            get
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(Requested_at, "dd-MM-yyyy HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
                    //return dt.ToString("yyyy-MM-dd");

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
                    return Requested_at;
                }

            }
        }

    }
}