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


            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");
            var customerResponse = Requestor.Post<CustomerResponse>(UrlsConstants.Customer, requestOptions , PostData);


            return true;
        }





    }
}