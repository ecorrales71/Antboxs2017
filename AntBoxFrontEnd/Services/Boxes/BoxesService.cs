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

namespace AntBoxFrontEnd.Services.Boxes
{
    public class BoxesService : Services
    {
        const int itemPerPage = 100;
        public int Page { get; set; }

        public BoxesService(string apiKey) : base(apiKey)
        {
            Page = 1;
        }

        public virtual Boolean CreateBoxes(BoxesRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.Box, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

        public virtual Boolean UpdateBox(BoxesUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.Box + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }



            return true;
        }


        public virtual BoxesResponse SearchBox(string id, RequestOptions requestOptions = null)
        {
            BoxesResponse box = new BoxesResponse();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = UrlHelper.BuildURLParametersString(parameters);

                box = Requestor.Get<BoxesResponse>(UrlsConstants.Box + "/" + id, requestOptions);

            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }

            return box;
        }


        public virtual List<BoxesResponse> ListBoxes(string status = null, RequestOptions requestOptions = null, string include = "price,label,model,size,secure")
        {
            PaginationBoxesResponse boxes = new PaginationBoxesResponse();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> ();

                if(string.IsNullOrEmpty(status))
                    status = StatusBoxes.Active;  //por defaul trae las activas solamente

                parameters.Add("status", status);

                parameters.Add("items_per_page", itemPerPage.ToString());
                parameters.Add("page_number", Page.ToString());
                parameters.Add("include", include);

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                boxes = Requestor.Get<PaginationBoxesResponse>(UrlsConstants.BoxesSearch + encodedParams, requestOptions);

            }catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
            return boxes.Boxes;
        }


        public virtual bool DeleteBoxes(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            try
            {
                Requestor.Delete(UrlsConstants.Box + "/" + id, requestOptions);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }


            return true;
        }


       

    }


    public static class StatusBoxes
    {
        public static readonly string Active = "active";

        public static readonly string Inactive = "inactive";

        public static readonly string All = "all";

    }

}