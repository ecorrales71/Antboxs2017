using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Services.Payments
{
    public class ChargeResponse : Response
    {

        [JsonProperty("status")]
        public string Status { get; set; }


        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("amount")]
        public string Amount { get; set; }


        [JsonProperty("creation_date")]
        public string Creation_date { get; set; }

        public string Fecha
        {
            get
            {
                return formatdate(Creation_date);
            }
        }

        public DateTime FechaObject
        {
            get
            {
                DateTime dt = Convert.ToDateTime(Creation_date);
                return dt;
            }
        }

        private string formatdate(string date)
        {
            try
            {
                string dateTime = date;
                DateTime dt = Convert.ToDateTime(dateTime);

                int month = dt.Month;
                string monthname = "";
                switch (month)
                {
                    case 1: monthname = "Enero"; break;
                    case 2: monthname = "Febrero"; break;
                    case 3: monthname = "Marzo"; break;
                    case 4: monthname = "Abril"; break;
                    case 5: monthname = "Mayo"; break;
                    case 6: monthname = "Junio"; break;
                    case 7: monthname = "Julio"; break;
                    case 8: monthname = "Agosto"; break;
                    case 9: monthname = "Septiembre"; break;
                    case 10: monthname = "Octubre"; break;
                    case 11: monthname = "Noviembre"; break;
                    case 12: monthname = "Diciembre"; break;

                }

                return dt.ToString(@"dd") + "/" + monthname + "/" + dt.ToString(@"yyyy");
            }
            catch
            {
                return date;
            }
        }

        public bool verifinmonth(DateTime dt)
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime firstDay = FirstDayOfMonth(today);
                DateTime lastDay = LastDayOfMonth(firstDay);

                if (dt.Ticks >= firstDay.Ticks && dt.Ticks <= lastDay.Ticks)
                {
                    return true;
                } else {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static DateTime FirstDayOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(DateTime dt)
        {
            return dt.AddMonths(1).AddDays(-1);
        }

    }
}