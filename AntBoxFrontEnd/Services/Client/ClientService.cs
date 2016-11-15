using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace AntBoxFrontEnd.Services.Client
{
    public class ClientService : Services
    {
        public ClientService(string apiKey) : base(apiKey)
        {
        }

        public virtual Boolean CreateClient(ClientRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var clientResponse = Requestor.Post<MissingError>(UrlsConstants.Client, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

    }
}