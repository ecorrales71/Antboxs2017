using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services;
using System.Text;
using AntBoxFrontEnd.Models;
using AutoMapper;


namespace AntBoxFrontEnd.Services.Tasks
{
    public class TaskService : Services
    {
        public TaskService(string apiKey) : base(apiKey)
        {
        }

        public virtual Boolean CreateTask(TaskRequestOption createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var task = Requestor.Post<TaskUpdateResponse>(UrlsConstants.Task, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log

                return false;
            }



            return true;
        }


        public virtual Boolean UpdateTask(TaskUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var task = Requestor.Put<TaskUpdateResponse>(UrlsConstants.Task + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log

                return false;
            }



            return true;
        }


        public virtual TaskResponse ListTaskByWorker(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            var task = Requestor.Get<TaskResponse>(UrlsConstants.Task + "/" + id, requestOptions);

            return task;
        }



    }


    public class ConstantsTaskStates
    {
        public string Unassigned { get { return "unassigned"; } }
        public string Assigned { get { return "assigned"; } }
        public string Active { get { return "active"; } }
        public string Completed { get { return "completed"; } }
    }

}