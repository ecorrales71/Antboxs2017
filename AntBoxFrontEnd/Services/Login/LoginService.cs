using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using System.Text;
using System.Net.Http;

namespace AntBoxFrontEnd.Services.Login
{
    public class LoginService : Services
    {
        public LoginService(string apiKey) : base(apiKey)
        {
        }

        public virtual string HovaLogin(LoginCreateOptions createOptions, RequestOptions requestOptions = null)
        {
            string id = null;

            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            var PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");
            var response = Requestor.Get<LoginResponse>(UrlsConstants.Login, requestOptions, PostData);



            return id;
        }


        public class LoginResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }


    }
}