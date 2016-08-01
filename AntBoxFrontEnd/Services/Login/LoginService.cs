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

            var parameters = new Dictionary<string, string> { { "email", createOptions.Email }, { "password", createOptions.Password} };

            var encodedContent = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            var response = Requestor.Get<LoginResponse>(UrlsConstants.Login + encodedContent, requestOptions);

            return response.Id;
        }


        public class LoginResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }


    }
}