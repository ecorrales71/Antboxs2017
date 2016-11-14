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

namespace AntBoxFrontEnd.Services.Billing
{
    public class BillingService : Services
    {

        const int itemPerPage = 40;

        const int Max = 3;

        public int Page { get; set; }


        public BillingService(string apiKey = null) : base(apiKey)
        {
            Page = 1;
        }

        public BillingService(int page, string apiKey = null)
            : base(apiKey)
        {
            Page = page;
        }


        public virtual Boolean CreateBilling(BillingRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            var customerResponse = new InsertResponse();
            try
            {
                customerResponse = Requestor.Post<InsertResponse>(UrlsConstants.Billing, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }
            return true;
        }


        public virtual Boolean UpdateBilling(BillingUpdateOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.Billing, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }



            return true;
        }

    }
}