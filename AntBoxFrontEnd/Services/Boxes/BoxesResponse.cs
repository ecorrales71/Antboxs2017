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
                return dt.ToString(@"dd\/MM\/yyyy");
            }
            catch
            {
                return date;
            }
        }


    }
}