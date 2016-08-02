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
        public CustomerServices(string apiKey) : base(apiKey)
        {
        }


        public virtual Boolean CreateCustomer(CustomerRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);            

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingResponse>(UrlsConstants.Customer, requestOptions, PostData);
            }catch (Exception ex)
            {
                //Todo log

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

                return false;
            }



            return true;
        }

        public virtual CustomerResponse SearchCustomer(string id , RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);
            
            var customers = Requestor.Get<CustomerResponse>(UrlsConstants.Customer + "/" + id, requestOptions);

            return customers;
        }




    }
}