using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using AntBoxFrontEnd.Services;
using AntBoxFrontEnd.Entities;
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


                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errbad = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.BadRequestResponse>(responseString);

                    if (errbad != null)
                        throw new Exception(errbad.Log);
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    var errMissing = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.MissingResponse>(responseString);

                    if (errMissing != null)
                        throw new Exception(errMissing.Missing);
                }

                if(responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var prueba = "prueba";
                }

                    throw new MissingException(responseMessage.StatusCode, new MissingError { Missing = responseString});
                
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
                

                if(responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errbad = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.BadRequestResponse>(responseString);

                    if (errbad != null)
                        throw new Exception(errbad.Log);
                }

                if(responseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    var errMissing = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.MissingResponse>(responseString);

                    if (errMissing != null)
                        throw new Exception(errMissing.Missing);
                }

                throw new MissingException(responseMessage.StatusCode,responseMessage.Content.ToString());
                
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

                if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errbad = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.BadRequestResponse>(responseString);

                    if (errbad == null)
                        throw new Exception(errbad.Log);
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    var errMissing = JsonConvert.DeserializeObject<AntBoxFrontEnd.Services.MissingResponse>(responseString);

                    if (errMissing == null)
                        throw new Exception(errMissing.Missing);
                }

                throw new MissingException(responseMessage.StatusCode, responseMessage.Content.ToString());
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

        internal static RestClient GetRestClient(RequestOptions requestOptions)
        {
            try
            {
                requestOptions.ApiKey = requestOptions.ApiKey ?? ServiceConfiguration.GetApiKey();
                var client = new RestClient();
                string hovaus = WebConfigurationManager.AppSettings["hovaid"];
                string hovapw = WebConfigurationManager.AppSettings["hovapw"];

                client.Authenticator = new HttpBasicAuthenticator(hovaus, hovapw);
                client.BaseUrl = new Uri(UrlsConstants.BaseUrl);

                return client;
            }catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }


    }
}