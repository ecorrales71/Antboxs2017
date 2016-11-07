using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Boxes
{
    public class BoxesResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }       

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("secure_label")]
        public string Secure_label { get; set; }

        [JsonProperty("secure")]
        public decimal Secure { get; set; }

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

        [JsonProperty("activation_date")]
        public string Activation_date { get; set; }

        [JsonProperty("slu")]
        public decimal? Slu { get; set; }

        [JsonProperty("registered_by")]
        public CustomerResponse Registered_by { get; set; }

        public string Fecha_activacion
        {
            get
            {
                return formatdate(Activation_date, "yyyy-MM-dd");

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