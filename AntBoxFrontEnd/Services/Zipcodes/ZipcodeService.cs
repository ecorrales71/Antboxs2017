using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Services;
using AntBoxFrontEnd.Infrastructure;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using AntBoxFrontEnd.Entities;

namespace AntBoxFrontEnd.Services.Zipcodes
{
    public class ZipcodeService : Services
    {
        const int itemPerPage = 10;
        public int Page { get; set; }

        public ZipcodeService(string apiKey) : base(apiKey)
        {
        }

        public List<ZipCodeResponse> SearchZipCode (string zipcode, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "code", zipcode} };

            var encodedContent = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            var zipResponse = Requestor.Get<List<ZipCodeResponse>>(UrlsConstants.ZipCode + encodedContent, requestOptions);

            return zipResponse;
        }

        public virtual Boolean CreateZipCode(ZipCodeRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.Code, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

        public virtual Boolean UpdateZipCode(ZipCodeUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.Code + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }



            return true;
        }

        public virtual List<ZipCodeResponse> ListZipCode(int currentPage, string idPagination = null, RequestOptions requestOptions = null)
        {
            List<ZipCodeResponse> coupons = new List<ZipCodeResponse>();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("items_per_page", itemPerPage.ToString());

                if (!string.IsNullOrEmpty(idPagination))
                {
                    parameters.Add("pagination_id", idPagination);
                    parameters.Add("page_number", currentPage.ToString());
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                coupons = Requestor.Get<List<ZipCodeResponse>>(UrlsConstants.CodeList + encodedParams, requestOptions);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
            return coupons;
        }

        public virtual bool DeleteZipCode(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            try
            {
                Requestor.Delete(UrlsConstants.Code + "/" + id, requestOptions);

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