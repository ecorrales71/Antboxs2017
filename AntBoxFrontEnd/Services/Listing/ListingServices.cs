using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Client;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
using AntBoxFrontEnd.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.Listing
{
    public class ListingServices : Services
    {

        public ListingServices(string apiKey) : base(apiKey)
        {
        }

        public virtual PaginationCustomerResponse ListCustomer(int? currentPage, string from, string to, string status, string total = null, string idPagination = null, RequestOptions requestOptions = null)
        {
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
                if (!string.IsNullOrEmpty(total))
                {
                    parameters.Add("items_per_page", total);
                }
                if (!string.IsNullOrEmpty(from))
                {
                    string[] words = from.Split('/');
                    from = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("from", from);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    string[] words = to.Split('/');
                    to = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("to", to);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    parameters.Add("status", status.ToString());
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var customers = Requestor.Get<PaginationCustomerResponse>(UrlsConstants.ListingCustomer + encodedParams, requestOptions);

                return customers;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationClientResponse ListClient(int? currentPage, string from, string to, string zipcode, string total = null, string idPagination = null, RequestOptions requestOptions = null)
        {
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
                if (!string.IsNullOrEmpty(total))
                {
                    parameters.Add("items_per_page", total);
                }
                if (!string.IsNullOrEmpty(from))
                {
                    string[] words = from.Split('/');
                    from = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("from", from);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    string[] words = to.Split('/');
                    to = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("to", to);
                }
                if (!string.IsNullOrEmpty(zipcode))
                {
                    parameters.Add("zipcode", zipcode.ToString());
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var clients = Requestor.Get<PaginationClientResponse>(UrlsConstants.ListingClient + encodedParams, requestOptions);

                return clients;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationCustomerReport ListCustomerReport(int? currentPage, string from, string to, string status, string total = null, string idPagination = null, RequestOptions requestOptions = null)
        {
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
                if (!string.IsNullOrEmpty(total))
                {
                    parameters.Add("items_per_page", total);
                }

                if (!string.IsNullOrEmpty(from))
                {
                    parameters.Add("from", from);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    parameters.Add("to", to);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    parameters.Add("status", status.ToString());
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var customers = Requestor.Get<PaginationCustomerReport>(UrlsConstants.ListingCustomer + encodedParams, requestOptions);

                return customers;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationPayments ListPayments(int? currentPage, string from, string to, string type, string total = null, string idPagination = null, RequestOptions requestOptions = null)
        {
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
                if (!string.IsNullOrEmpty(total))
                {
                    parameters.Add("items_per_page", total);
                }

                if (!string.IsNullOrEmpty(from))
                {
                    string[] words = from.Split('/');
                    from = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("from", from);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    string[] words = to.Split('/');
                    to = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("to", to);
                }
                if (!string.IsNullOrEmpty(type))
                {
                    parameters.Add("type", type);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var payments = Requestor.Get<PaginationPayments>(UrlsConstants.ListingPayments + encodedParams, requestOptions);

                return payments;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationPickup ListPickups(int? currentPage, string from, string to, string status, string total = null, string idPagination = null, RequestOptions requestOptions = null)
        {
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
                if (!string.IsNullOrEmpty(total))
                {
                    parameters.Add("items_per_page", total);
                }

                if (!string.IsNullOrEmpty(from))
                {
                    string[] words = from.Split('/');
                    from = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("from", from);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    string[] words = to.Split('/');
                    to = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("to", to);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    parameters.Add("status", status);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var pickups = Requestor.Get<PaginationPickup>(UrlsConstants.ListingPickups + encodedParams, requestOptions);

                return pickups;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationDelivery ListDeliveries(int? currentPage, string from, string to, string status, string total = null, string idPagination = null, RequestOptions requestOptions = null)
        {
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
                if (!string.IsNullOrEmpty(total))
                {
                    parameters.Add("items_per_page", total);
                }

                if (!string.IsNullOrEmpty(from))
                {
                    string[] words = from.Split('/');
                    from = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("from", from);
                }
                if (!string.IsNullOrEmpty(to))
                {
                    string[] words = to.Split('/');
                    to = words[2] + '-' + words[1] + '-' + words[0];
                    parameters.Add("to", to);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    parameters.Add("status", status);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var deliveries = Requestor.Get<PaginationDelivery>(UrlsConstants.ListingDeliveries + encodedParams, requestOptions);

                return deliveries;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }
    }
}