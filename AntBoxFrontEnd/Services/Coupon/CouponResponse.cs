using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Coupon
{
    public class CouponResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }       

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

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

        public string Fecha_creacion
        {
            get
            {
                return formatdate(Creation_date, "dd-MM-yyyy HH:mm:ss");
            }
        }

        public string Inicio
        {
            get
            {
                return formatdate(From, "yyyy-MM-dd");
            }
        }

        public string Vigencia
        {
            get
            {
                return formatdate(To, "yyyy-MM-dd");
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