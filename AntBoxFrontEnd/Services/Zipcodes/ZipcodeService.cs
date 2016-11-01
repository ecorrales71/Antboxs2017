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

        public List<ZipCodeResponse> SearchZipCode (string zipcode, string neighborhood = null, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "code", zipcode} };
            if (!string.IsNullOrEmpty(neighborhood))
            {
                parameters.Add("neighborhood", neighborhood);
            }
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
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.ZipCode, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }
            return true;
        }

        public virtual Boolean CreateZipCodes(string createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            StringContent PostData = new StringContent(createOptions, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.ZipCode, requestOptions, PostData);
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
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.ZipCode + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);

                return false;
            }



            return true;
        }

        public virtual PaginationZipCodesResponse ListZipCode(int? currentPage, string codigo, string estado, string municipio, string colonia, string registro, string status, string idPagination = null, RequestOptions requestOptions = null)
        {
            PaginationZipCodesResponse coupons = new PaginationZipCodesResponse();

            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                if (currentPage == null)
                {
                    currentPage = 1;
                }
                if (!string.IsNullOrEmpty(idPagination))
                {
                    parameters.Add("pagination_id", idPagination);
                    parameters.Add("page_number", currentPage.ToString());
                }
                if (!string.IsNullOrEmpty(codigo))
                {
                    parameters.Add("code", codigo);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "Activo")
                    {
                        parameters.Add("status", "true");
                    } else if (status == "Inactivo")
                    {
                        parameters.Add("status", "false");
                    }
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
                parameters.Add("enable_pagination", "true");

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                coupons = Requestor.Get<PaginationZipCodesResponse>(UrlsConstants.Zipcode + encodedParams, requestOptions);

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
                Requestor.Delete(UrlsConstants.Zipcode + "/" + id, requestOptions);

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