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

namespace AntBoxFrontEnd.Services.Address
{
    public class AddressService : Services
    {

        const int itemPerPage = 10;
        public int Page { get; set; }


        public AddressService(string apiKey = null) : base(apiKey)
        {
            Page = 1;
        }

        public AddressService(int page, string apiKey = null) : base(apiKey)
        {
            Page = page;
        }


        public virtual Boolean CreateAddress(AddressRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Post<MissingError>(UrlsConstants.CustomerAddress, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log

                return false;
            }
            return true;
        }


        public virtual Boolean UpdateAddress(AddressUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.CustomerAddress + encodedParams, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log

                return false;
            }



            return true;
        }



        public virtual AddressResponse SearchAddress(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            var addresses = Requestor.Get<AddressResponse>(UrlsConstants.CustomerAddress + encodedParams, requestOptions);

            return addresses;
        }


        public virtual List<AddressResponse> ListAddresses(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "customer_id", id } };

            parameters.Add("items_per_page", itemPerPage.ToString());
            parameters.Add("page_number", Page.ToString());

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            var addresses = Requestor.Get<PaginationAddresses>(UrlsConstants.CustomerAddress + encodedParams, requestOptions);
            
            return addresses.ListAddresses;
        }



    }
}