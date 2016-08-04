using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using AntBoxFrontEnd.Services;
using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Models;
using System.Web.Configuration;
using RestSharp;
using RestSharp.Authenticators;


namespace AntBoxFrontEnd.Infrastructure
{
    internal class Requestor
    {
        
        public static T Get<T>(string url, RequestOptions requestOptions)
        {
            using (var client = new HttpClient())
            {
                requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", requestOptions.ApiKey);
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage;
                    
                responseMessage = client.GetAsync(url).Result;
                    

                var responseString = responseMessage.Content.ReadAsStringAsync().Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    T obj = JsonConvert.DeserializeObject<T>(responseString);
                    return obj;
                }
                else
                {
                    var err = JsonConvert.DeserializeObject<MissingError>(responseString);

                    if (err == null)
                        throw new Exception(responseString);

                    throw new MissingException(responseMessage.StatusCode, err);
                }
            }
        }

        public static T Post<T>(string path, RequestOptions requestOptions,  StringContent content = null)
        {
            using(var client = new HttpClient())
            {
                requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", requestOptions.ApiKey);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync(path, content);
                var responseMessage = response.Result;
                var responseString = responseMessage.Content.ReadAsStringAsync().Result;


                if (responseMessage.IsSuccessStatusCode)
                {
                    T obj = JsonConvert.DeserializeObject<T>(responseString);
                    return obj;
                }
                else
                {

                    if(responseMessage.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var errbad = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.Tasks.TaskUpdateResponse>(responseString);

                        if (errbad == null)
                            throw new Exception(errbad.Log);
                    }



                    var err = JsonConvert.DeserializeObject<MissingError>(responseString);

                    if (err == null)
                        throw new Exception(responseString);

                    throw new MissingException(responseMessage.StatusCode, err);
                }
            }            
        }

        public static T Put<T>(string url, RequestOptions requestOptions, StringContent content = null)
        {
            using (var client = new HttpClient())
            {
                requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", requestOptions.ApiKey);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.PutAsync(url, content).Result;
                var responseString = responseMessage.Content.ReadAsStringAsync().Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    T obj = JsonConvert.DeserializeObject<T>(responseString);
                    return obj;
                }
                else
                {
                    var err = JsonConvert.DeserializeObject<MissingError>(responseString);

                    if (err == null)
                        throw new Exception(responseString);

                    throw new MissingException(responseMessage.StatusCode, err);
                }
            }
        }

        public static void Delete(string url, RequestOptions requestOptions)
        {
            using (var client = new HttpClient())
            {
                requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", requestOptions.ApiKey);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseMessage = client.DeleteAsync(url).Result;
                var responseString = responseMessage.Content.ReadAsStringAsync().Result;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var err = JsonConvert.DeserializeObject<MissingError>(responseString);

                    if (err == null)
                        throw new Exception(responseString);

                    throw new MissingException(responseMessage.StatusCode, err);
                }
            }

        }

        //internal static HttpClient GetHttpClient(RequestOptions requestOptions)
        //{
        //    requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
        //    HttpClient client = new HttpClient();
        //    //var byteArray = Encoding.ASCII.GetBytes(requestOptions.ApiKey);
        //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", requestOptions.ApiKey );
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    return client;
        //}


        internal static RestClient GetRestClient(RequestOptions requestOptions)
        {
            requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
            var client = new RestClient();
            string hovaus = WebConfigurationManager.AppSettings["hovaid"];
            string hovapw = WebConfigurationManager.AppSettings["hovapw"];

            client.Authenticator = new HttpBasicAuthenticator(hovaus, hovapw);
            client.BaseUrl = new Uri(UrlsConstants.BaseUrl);

            return client;
        }


    }
}