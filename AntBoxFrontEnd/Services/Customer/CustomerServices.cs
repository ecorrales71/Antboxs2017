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

namespace AntBoxFrontEnd.Services.Customer
{
    public class CustomerServices : Services
    {
        const int itemPerPage = 100;
        public int Page { get; set; }

        public CustomerServices(string apiKey) : base(apiKey)
        {
        }


        public virtual Boolean CreateCustomer(CustomerRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            if (createOptions.Status == null)
                createOptions.Status = true;
                      

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {

                var customerResponse = Requestor.Post<CustomerCreatedResponse>(UrlsConstants.Customer, requestOptions, PostData);
                if (string.IsNullOrEmpty(customerResponse.Id))
                    return false;

            }catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return false;
            }
            


            return true;
        }



        public virtual Boolean UpdateCustomer(CustomerUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<CustomerResponse>(UrlsConstants.Customer + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return false;
            }



            return true;
        }

        public virtual CustomerResponse SearchCustomer(string id , RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var customers = Requestor.Get<CustomerResponse>(UrlsConstants.Customer + "/" + id, requestOptions);

                return customers;
            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return null;
            }
        }


        public virtual PaginationCustomerResponse ListCustomer(int currentPage, string idPagination = null, RequestOptions requestOptions = null)
        {
            PaginationCustomerResponse customers = new PaginationCustomerResponse();

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

                customers = Requestor.Get<PaginationCustomerResponse>(UrlsConstants.CustomerList + encodedParams, requestOptions);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
            return customers;
        }

    }

    public class CustomerCreatedResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}