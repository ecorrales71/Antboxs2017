using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using System.Text;
using System.Net.Http;
using AntBoxFrontEnd.Entities;

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

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "email", createOptions.Email }, { "password", createOptions.Password } };

                var encodedContent = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var response = Requestor.Get<LoginResponse>(UrlsConstants.Login + encodedContent, requestOptions);

                id = response.Id;
            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }

            return id;
        }

        public virtual LoginResponse HovaLoginObject(LoginCreateOptions createOptions, RequestOptions requestOptions = null)
        {
            LoginResponse response = null;

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "email", createOptions.Email }, { "password", createOptions.Password } };

                var encodedContent = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                response = Requestor.Get<LoginResponse>(UrlsConstants.Login + encodedContent, requestOptions);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }

            return response;
        }

        public virtual Boolean ResetPassword(ResetCreateOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.ResetPassword, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

        public virtual Boolean RestorePassword(RestoreCreateOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.RestorePassword, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

        public class LoginResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            [JsonProperty("worker_id")]
            public string Worker_id { get; set; }
            [JsonProperty("role")]
            public string Role { get; set; }
        }


    }
}