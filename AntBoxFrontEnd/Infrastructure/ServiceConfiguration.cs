using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using RestSharp;
using RestSharp.Authenticators;
using AntBoxFrontEnd.Entities;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Infrastructure
{
    public class ServiceConfiguration
    {
        private static string _apiKey;
        internal const string SupportedApiVersion = "v1";

        static ServiceConfiguration()
        {
            ApiVersion = SupportedApiVersion;
        }

        internal static string GetApiKey()
        {
            //if (string.IsNullOrEmpty(_apiKey))
            //{

                string hovaus = WebConfigurationManager.AppSettings["hovaid"];

                string hovapw = WebConfigurationManager.AppSettings["hovapw"];

                HovaAuthResponse hovaAuth = null;

                RestRequest request = new RestRequest(Method.POST);

                var client = new RestClient(UrlsConstants.AuthUrl);

                client.BaseUrl = new Uri(UrlsConstants.AuthUrl);

                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                client.Authenticator = new HttpBasicAuthenticator(hovaus, hovapw);
                request.AddHeader("Accept", "application/json");

                request.AddParameter("grant_type", "password");
                request.AddParameter("password", hovapw);
                request.AddParameter("username", hovaus);
                request.AddParameter("scope", "core");

                try
                {
                    var response = client.Execute(request);


                    if (response.ResponseStatus != ResponseStatus.Completed)
                    {
                        var error = response.Content;

                        throw new Exception(response.StatusCode + " " + error);
                    }

                    var result = response.Content;

                    hovaAuth = JsonConvert.DeserializeObject<HovaAuthResponse>(result);


                    if (hovaAuth == null || String.IsNullOrEmpty(hovaAuth.Access_token))
                        return null;

                    _apiKey = hovaAuth.Access_token;

                }
                catch (Exception ex)
                {
                    // Log

                    return null;

                }
                
            //}

            return _apiKey;
        }


   

        public static void SetApiKey(string newApiKey)
        {
            _apiKey = newApiKey;
        }

        public static string ApiVersion { get; internal set; }
    }
}
