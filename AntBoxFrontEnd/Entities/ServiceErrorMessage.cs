using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Entities
{
    public class ServiceErrorMessage
    {
        //[JsonProperty("error")]
        //public int ErrorNumber { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        //[JsonProperty("docs")]
        //public string Docs { get; set; }

        [JsonProperty("request")]
        public RequestErrorType Request { get; set; }

        [JsonProperty("cause")]
        public object Cause { get; set; }
    }



    public enum RequestErrorType
    {
        Post,
        Get,
        Delete
    }
}