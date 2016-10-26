using AntBoxFrontEnd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Services.CustomerService
{
    public class CSServices : Services
    {

        public CSServices(string apiKey) : base(apiKey)
        {
        }

        public virtual PaginationDelivery ListDeliveries(int? currentPage, string name, string tipo, string operador, string antboxs, string fecha_solicitud, string fecha_entrega, string horario, string status, string idPagination = null, RequestOptions requestOptions = null)
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

                if (!string.IsNullOrEmpty(name))
                {
                    parameters.Add("name", name);
                }
                if (!string.IsNullOrEmpty(fecha_solicitud))
                {
                    parameters.Add("requested_at", fecha_solicitud);
                }
                if (!string.IsNullOrEmpty(fecha_entrega))
                {
                    parameters.Add("delivery_at_date", fecha_entrega);
                }
                if (!string.IsNullOrEmpty(horario))
                {
                    parameters.Add("delivery_at_time", horario);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    parameters.Add("delivery_at_time", status);
                }
                if (!string.IsNullOrEmpty(antboxs))
                {
                    parameters.Add("antboxs", antboxs);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var deliveries = Requestor.Get<PaginationDelivery>(UrlsConstants.DeliveryListCS + encodedParams, requestOptions);

                return deliveries;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationPickup ListPickups(int? currentPage, string name, string tipo, string operador, string antboxs, string fecha_solicitud, string fecha_recoleccion, string horario, string status, string idPagination = null, RequestOptions requestOptions = null)
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

                if (!string.IsNullOrEmpty(name))
                {
                    parameters.Add("name", name);
                }
                if (!string.IsNullOrEmpty(fecha_solicitud))
                {
                    parameters.Add("requested_at", fecha_solicitud);
                }
                if (!string.IsNullOrEmpty(fecha_recoleccion))
                {
                    parameters.Add("pickup_at_date", fecha_recoleccion);
                }
                if (!string.IsNullOrEmpty(horario))
                {
                    parameters.Add("pickup_at_time", horario);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    parameters.Add("delivery_at_time", status);
                }
                if (!string.IsNullOrEmpty(antboxs))
                {
                    parameters.Add("antboxs", antboxs);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var pickups = Requestor.Get<PaginationPickup>(UrlsConstants.PickupListCS + encodedParams, requestOptions);

                return pickups;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }

        public virtual PaginationOrder ListOrders(int? currentPage, string name, string pedido, string codigo, string recoleccion, string registro, string idPagination = null, RequestOptions requestOptions = null)
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

                if (!string.IsNullOrEmpty(name))
                {
                    parameters.Add("name", name);
                }
                if (!string.IsNullOrEmpty(pedido))
                {
                    parameters.Add("folio", pedido);
                }
                if (!string.IsNullOrEmpty(codigo))
                {
                    parameters.Add("code", codigo);
                }
                if (!string.IsNullOrEmpty(recoleccion))
                {
                    parameters.Add("pickup", recoleccion);
                }
                if (!string.IsNullOrEmpty(registro))
                {
                    parameters.Add("registered_at", registro);
                }

                var encodedParams = Infrastructure.UrlHelper.BuildURLParametersString(parameters);

                var orders = Requestor.Get<PaginationOrder>(UrlsConstants.OrderListCS + encodedParams, requestOptions);

                return orders;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
                return null;
            }
        }
    }
}