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
        public readonly string Type_Delivery = "delivery";
        public readonly string Type_Pickup = "pickup";

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
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
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
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }



            return true;
        }


        public virtual ListTask ListTaskByWorker(string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                //var parameters = new Dictionary<string, string> { { "id", id } };

               // var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var task = Requestor.Get<ListTask>(UrlsConstants.Task + "/" + id, requestOptions);

                return task;
            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                var empty = new ListTask();
                return empty;
            }
        }

        public virtual List<Schedules> ListSchedules(DateTime date, RequestOptions requestOptions = null)
        {
            try
            {

                var dateString = date.ToString("yyyy-MM-dd");


                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "date", dateString } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var schedules = Requestor.Get<ScheduleResponse>(UrlsConstants.Schedules + encodedParams, requestOptions);

                if (schedules.Schedules.Count > 0)
                {
                    schedules.Schedules.ForEach(x => {
                        x.worker = x.Workers.FirstOrDefault();
                    });
                }
                return schedules.Schedules;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }


        public virtual List<Schedules> ListSchedules(string dateString, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "date", dateString } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var schedules = Requestor.Get<ScheduleResponse>(UrlsConstants.Schedules + encodedParams, requestOptions);

                if (schedules.Schedules.Count > 0)
                {
                    schedules.Schedules.ForEach(x => {
                        x.worker = x.Workers.FirstOrDefault();
                    });
                }
                return schedules.Schedules;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }


    }


    public static class ConstantsTaskStates
    {
        public static string Unassigned { get { return "unassigned"; } }
        public static string Assigned { get { return "assigned"; } }
        public static string Active { get { return "active"; } }
        public static string Completed { get { return "completed"; } }
    }

}