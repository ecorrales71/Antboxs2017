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

        public virtual Boolean UpdateUser(UserUpdateOptions createOptions, string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            var parameters = new Dictionary<string, string> { { "id", id } };

            var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

            string serilizedObj = JsonConvert.SerializeObject(createOptions, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }).ToString();
            StringContent PostData = new StringContent(serilizedObj, Encoding.UTF8, "application/json");

            try
            {
                var userResponse = Requestor.Put<MissingResponse>(UrlsConstants.User + "/" + id, requestOptions, PostData);
            }
            catch (Exception ex)
            {
                //Todo log
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return false;
            }

            return true;
        }

        public virtual PaginationUser ListUsersPagination(int? currentPage, string idPagination = null, RequestOptions requestOptions = null)
        {
            try
            {
                requestOptions = SetupRequestOptions(requestOptions);

                var parameters = new Dictionary<string, string>();

                parameters.Add("role", "all");

                if (!string.IsNullOrEmpty(idPagination))
                {
                    parameters.Add("pagination_id", idPagination);
                    parameters.Add("page_number", currentPage.ToString());
                }
                

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var users = Requestor.Get<PaginationUser>(UrlsConstants.UserList + encodedParams, requestOptions);

                return users;
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

                var user = Requestor.Get<UserResponse>(UrlsConstants.User + "/" + id, requestOptions);

                return user;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogType.Error);
                return null;
            }
        }

        public virtual bool DeleteUser(string id, RequestOptions requestOptions = null)
        {
            requestOptions = SetupRequestOptions(requestOptions);

            try
            {
                Requestor.Delete(UrlsConstants.User + "/" + id, requestOptions);

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