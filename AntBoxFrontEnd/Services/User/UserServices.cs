using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace AntBoxFrontEnd.Services.User
{
    public class UserServices : Services
    {
        const int itemPerPage = 20;
        public int Page { get; set; }

        public UserServices(string apiKey = null) : base(apiKey)
        {
            Page = 1;
        }

        public UserServices(int page, string apiKey = null)
            : base(apiKey)
        {
            Page = page;
        }

        public virtual Boolean CreateUser(UserRequestOptions createOptions, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            if (createOptions.Change_password == null)
                createOptions.Change_password = false;


            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {

                var customerResponse = Requestor.Post<InsertResponse>(UrlsConstants.User, requestOptions, PostData);
                if (string.IsNullOrEmpty(customerResponse.Id))
                    return false;

            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return false;
            }



            return true;
        }

        public virtual PaginationUser ListUsersPagination(int currentPage, string idPagination = null, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("role", "all");

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var addresses = Requestor.Get<PaginationUser>(UrlsConstants.UserList + encodedParams, requestOptions);

                return addresses;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual UserResponse SearchUser(string id, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string> { { "id", id } };

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var customers = Requestor.Get<UserResponse>(UrlsConstants.User + "/" + id, requestOptions);

                return customers;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return null;
            }
        }
        
    }

    
}