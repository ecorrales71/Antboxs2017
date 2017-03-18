using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Services.Address;
using Newtonsoft.Json.Linq;

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerListingResponse : Response
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname2")]
        public string Lastname2 { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        public string Namev
        {
            get
            {
                string cad = "";
                if (!string.IsNullOrEmpty(Name) && Name != "undefined")
                {
                    cad = Name;
                }
                if (!string.IsNullOrEmpty(Lastname) && Lastname != "undefined") {
                    cad = cad + " " + Lastname;
                }
                if (!string.IsNullOrEmpty(Lastname2) && Lastname2 != "undefined")
                {
                    cad = cad + " " + Lastname2;
                }
                return cad;
            }
        }

        [JsonProperty("lastname2v")]
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

        [JsonProperty("member_since")]
        public string Member_since { get; set; }

        [JsonProperty("status")]
        public bool? Status { get; set; }

        [JsonProperty("rfc_id")]
        public string Rfc_id { get; set; }

        [JsonProperty("addresses")]
        public int Addresses { get; set; }

        [JsonProperty("address")]
        public dynamic Address { get; set; }

        public string Direccion
        {
            get
            {
                try
                {
                    if (Address is JArray)
                    {
                        return "";
                    }
                    else
                    {
                        AddressResponse aux = Address.ToObject<AddressResponse>();
                        return aux.Street + " " + aux.External_number + ", " + aux.Neighborhood + ", " + aux.City + ", C.P. " + aux.Zipcode;
                    }
                }
                catch (Exception ex)
                {
                    return "";
                }
                    
            }
        }

        [JsonProperty("cards")]
        public int Cards { get; set; }

        public string Rfcv
        {
            get
            {
                if (Rfc_id == "undefined")
                {
                    return "";
                }
                else return Rfc_id;
            }
            
        }

        public string MiembroDesde
        {
            get
            {
                return formatdate(Member_since, "dd-MM-yyyy HH:mm:ss");

            }
        }

        [JsonProperty("antboxs")]
        public int Antboxsnumber { get; set; }

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

        public string Activo
        {
            get
            {
                if (Status == true)
                {
                    return "Si";
                }
                else
                {
                    return "No";
                }
            }
        }
    }

    public class CustomerListingReport : Response
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        public string Emailv
        {
            get
            {
                if (string.IsNullOrEmpty(Email))
                {
                    return "";
                }
                else
                {
                    return Email;
                }
            }
        }
    }

    public class CustomerListingReportMap : Response
    {
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Emailv { get; set; }
    }
}