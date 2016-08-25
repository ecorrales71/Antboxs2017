using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Entities;
using System.Text;
using AntBoxFrontEnd.Models;
using AutoMapper;

namespace AntBoxFrontEnd.Services.Worker
{
    public class WorkerService : Services
    {
        public WorkerService(string apiKey) : base(apiKey)
        {
        }


        public virtual Boolean CreateWorker(WorkerRequestOption createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingResponse>(UrlsConstants.Worker, requestOptions, PostData);

            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return false;
            }
            return true;
        }

        public virtual bool UpdateWorker(WorkerUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingError>(UrlsConstants.Worker + "/" + id, requestOptions, PostData);
                return true;
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return false;
            }
        }



        public virtual WorkerResponse SearchWorker(string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var customers = Requestor.Get<WorkerResponse>(UrlsConstants.Worker + "/" + id, requestOptions);

                return customers;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return null;
            }
        }

    }
}