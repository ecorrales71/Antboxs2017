using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Boxes;

namespace AntBoxFrontEnd.Services.AntBoxes
{
    public class AntBoxResponse
    {

        [JsonProperty("id")]
        public string Id { get; set; }


        [JsonProperty("serial")]
        public string Serial { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description
        {
            get;
            set;
        }

        public string Descriptionv
        {
            get
            {
                if ((Description == "undefined") || (Description == "Descripci�n"))
                {
                    return "Descripción";
                }
                else
                {
                    return Description;
                }
            }
        }

        [JsonProperty("folio")]
        public string Folio { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("box_id")]
        public string Box_id { get; set; }

        [JsonProperty("padlocks")]
        public List<string> Padlocks { get; set; }

        [JsonProperty("box")]
        public BoxesResponse Box { get; set; }

        public string EnDeposito
        {
            get
            {
                if (Status == "inhouse")
                {
                    return "- En Depósito";
                }
                else
                {
                    return "";
                }
            }
        }

        public string imagen
        {
            get
            {
                string imagen = UrlsConstants.HostPublic + "/assets/" + Id + ".png";
                if (URLExists(imagen))
                {
                    return imagen;
                }
                else
                {
                    return "/images/antboxs_3.jpg";
                }
            }

        }

        public bool URLExists(string url)
        {
            bool result = false;

            WebRequest webRequest = WebRequest.Create(url);
            webRequest.Timeout = 1200; // miliseconds
            webRequest.Method = "HEAD";

            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                result = true;
            }
            catch (WebException webException)
            {
                //Debug.Log(url + " doesn't exist: " + webException.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }


    }
}