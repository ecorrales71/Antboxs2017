using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Customer;

namespace AntBoxFrontEnd.Services.CustomerService
{
    public class DeliveryResponse : Response
    {

        [JsonProperty("customer_id")]
        public string Customer_id { get; set; }

        [JsonProperty("address_id")]
        public string Address_id { get; set; }

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

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("antboxs")]
        public string Antboxs { get; set; }

        [JsonProperty("requested_at")]
        public string Requested_at { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("worker")]
        public WorkerResponse Worker { get; set; }

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

        public string Solicitud
        {
            get
            {
                return formatdate(Requested_at, "dd-MM-yyyy HH:mm:ss");

            }
        }

        public string Entrega
        {
            get
            {
                return formatdate(Date, "yyyy-MM-dd");

            }
        }

        [JsonProperty("time")]
        public string Time { get; set; }

        public string Estatus
        {
            get
            {
                if (Status == "inprogress")
                {
                    return "En progreso";
                }
                else
                {
                    return "Completado";
                }
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