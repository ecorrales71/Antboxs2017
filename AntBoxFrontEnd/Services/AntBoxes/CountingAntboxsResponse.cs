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
    public class CountingAntboxsResponse
    {

        [JsonProperty("antboxs")]
        public int Antboxs { get; set; }

        
    }
}