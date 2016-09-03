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

        const int itemPerPage = 3;
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
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
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
                var customerResponse = Requestor.Put<MissingResponse>(UrlsConstants.CustomerAddress + "/" + id, requestOptions, PostData);
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
        public virtual AddressResponse SearchAddress(string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var addresses = Requestor.Get<AddressResponse>(UrlsConstants.CustomerAddress + "/" + id, requestOptions);

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
        public virtual List<AddressResponse> ListAddresses(string id, int currentPage  = 1, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("items_per_page", itemPerPage.ToString());
                parameters.Add("page_number", currentPage.ToString());

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var addresses = Requestor.Get<PaginationAddresses>(UrlsConstants.CustomerAddressSearch + "/" + id + encodedParams, requestOptions);

                return addresses.Addresses;
            }catch(Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }


        public virtual bool DeleteAddress(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            try
            {
                Requestor.Delete(UrlsConstants.CustomerAddress + "/" + id, requestOptions);

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