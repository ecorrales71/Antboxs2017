using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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


    }
}