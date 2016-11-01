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


using System.Threading.Tasks;
using AntBoxFrontEnd.Services.Util;

namespace AntBoxFrontEnd.Services.BillingAddress
{
    public class BillingAddressService : Services
    {

        const int itemPerPage = 40;

        const int Max = 3;

        public int Page { get; set; }


        public BillingAddressService(string apiKey = null) : base(apiKey)
        {
            Page = 1;
        }

        public BillingAddressService(int page, string apiKey = null)
            : base(apiKey)
        {
            Page = page;
        }


        public virtual InsertResponse CreateBillingAddress(BillingAddressRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            var customerResponse = new InsertResponse();
            try
            {
                customerResponse = Requestor.Post<InsertResponse>(UrlsConstants.BillingAddress, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
            return customerResponse;
        }

        public virtual Boolean CreateBillingAddressForCustomer(BillingAddressRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingResponse>(UrlsConstants.BillingAddress, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }
            return true;
        }


        public virtual Boolean UpdateBillingAddress(BillingAddressUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.BillingAddress + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }



            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Address  ID</param>
        /// <param name="requestOptions"></param>
        /// <returns></returns>
        public virtual BillingAddressResponse SearchBillingAddress(string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var addresses = Requestor.Get<BillingAddressResponse>(UrlsConstants.BillingAddress + "/" + id, requestOptions);

                return addresses;
            }catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="requestOptions"></param>
        /// <returns></returns>
        public virtual List<BillingAddressResponse> ListBillingAddresses(string id, int currentPage = 1, string idPagination = null, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("items_per_page", itemPerPage.ToString());
                parameters.Add("include", "alias,bussiness_name,rfc_id,references");

                if (!string.IsNullOrEmpty(idPagination))
                {
                    parameters.Add("page_number", currentPage.ToString());
                    parameters.Add("pagination_id", idPagination);
                }else
                    parameters.Add("page_number", "1");


                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var addresses = Requestor.Get<PaginationBillingAddresses>(UrlsConstants.CustomerBillingAddressSearch + "/" + id + encodedParams, requestOptions);

                return addresses.Addresses;
            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }


        public virtual PaginationBillingAddresses ListBillingAddressesPagination(string id, int currentPage, string idPagination, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("items_per_page", Max.ToString());
                parameters.Add("page_number", currentPage.ToString());

                parameters.Add("pagination_id", idPagination);

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var addresses = Requestor.Get<PaginationBillingAddresses>(UrlsConstants.CustomerBillingAddressSearch + "/" + id + encodedParams, requestOptions);

                return addresses;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }


        public virtual bool DeleteBillingAddress(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            try
            {
                Requestor.Delete(UrlsConstants.BillingAddress + "/" + id, requestOptions);

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