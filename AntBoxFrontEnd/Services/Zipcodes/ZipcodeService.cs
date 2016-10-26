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

        public virtual List<ZipCodeResponse> ListZipCode(int? currentPage, string codigo, string estado, string municipio, string colonia, string registro, bool? status, string idPagination = null, RequestOptions requestOptions = null)
        {
            List<ZipCodeResponse> coupons = new List<ZipCodeResponse>();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                if (!string.IsNullOrEmpty(codigo))
                {
                    parameters.Add("code", codigo);
                }
                if (status != null)
                {
                    parameters.Add("status", status.ToString());
                }
                if (!string.IsNullOrEmpty(estado))
                {
                    parameters.Add("state", estado);
                }
                if (!string.IsNullOrEmpty(municipio))
                {
                    parameters.Add("delegation", municipio);
                }
                if (!string.IsNullOrEmpty(colonia))
                {
                    parameters.Add("neighborhood", colonia);
                }
                if (!string.IsNullOrEmpty(registro))
                {
                    parameters.Add("registered_at", registro);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                coupons = Requestor.Get<List<ZipCodeResponse>>(UrlsConstants.Zipcode + encodedParams, requestOptions);

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