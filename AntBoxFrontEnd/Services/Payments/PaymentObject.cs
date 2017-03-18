using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class PaymentObject
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        public string Lastname2v
        {
            get
            {
                if (Lastname2 == "undefined")
                {
                    return "";
                }
                else
                {
                    return Lastname2;
                }
            }
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("rfc")]
        public string Rfc { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        public string Monto
        {
            get
            {
                if (Type == "refund")
                {
                    return "-" + Amount;
                }
                else
                {
                    return Amount;
                }
            }
        }

        [JsonProperty("payment_id")]
        public string Payment_id { get; set; }

        [JsonProperty("charge_date")]
        public string Charge_date { get; set; }

        [JsonProperty("antboxs")]
        public int Antboxs { get; set; }

        public string Fecha
        {
            get
            {
                return formatdate(Charge_date, "dd-MM-yyyy HH:mm:ss");
            }
        }

        public string Tipo
        {
            get
            {
                var value = "";
                switch (Type)
                {
                    case "payment":
                        value = "Pago"; break;
                    case "refund":
                        value = "Devolución"; break;
                    case "deposit":
                        value = "Depósito"; break;
                    case "failed":
                        value = "Fallado"; break;
                }
                return value;
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

                return dt.ToString(@"dd") + "/" + monthname + "/" + dt.ToString(@"yyyy") + " " + dt.ToString(@"HH") + ":" + dt.ToString(@"mm") + ":" + dt.ToString(@"ss");
            }
            catch
            {
                return date;
            }
        }
    }
}
