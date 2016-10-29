using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AntBoxFrontEnd.Controllers
{
    [RedirectingActionHelpDesk]
    public class CustomerServiceController : Controller
    {
        // Listado de clientes
        // GET: /CustomerService/
        public ActionResult Index(string name, string email, string rfc, int? antboxs, string status, int? page, string idpagination, string vp)
        {
            var servicio = new CustomerServices(ServiceConfiguration.GetApiKey());

            CustomerListModel model = new CustomerListModel();
            PaginationCustomerResponse result = new PaginationCustomerResponse();

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                model.Page = 1;
                idpagination = null;
            }
            else
            {
                model.Page = page;
            }

            try
            {
                result = servicio.ListCustomer(page, name, email, rfc, antboxs, status, idpagination);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationCustomerResponse();
            }
            model.Customers = result;
            model.Name = name;
            model.Email = email;
            model.Rfc_id = rfc;
            model.Antboxs = antboxs;
            model.Status = status;
            return View(model);
        }

        public ActionResult Antboxs(string name, string pedido, string codigo, string registro, string recoleccion, string entrega, string status, int? page, string idpagination, string vp)
        {
            var servicio = new CSServices(ServiceConfiguration.GetApiKey());

            OrderListModel model = new OrderListModel();

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                model.Page = 1;
                idpagination = null;
            }
            else
            {
                model.Page = page;
            }

            PaginationOrder result = new PaginationOrder();
            try
            {
                result = servicio.ListOrders(page, name, pedido, codigo, recoleccion, registro, idpagination);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationOrder();
            }

            model.Orders = result;
            model.Name = name;
            model.Pedido = pedido;
            model.Codigo = codigo;
            model.Registro = registro;
            model.Recoleccion = recoleccion;
            model.Entrega = entrega;
            model.Status = status;

            return View(model);
        }

        public ActionResult Entregas(string name, string tipo, string operador, string antboxs, string fecha_solicitud, string fecha_entrega, string horario, string status, int? page, string idpagination, string vp)
        {
            var servicio = new CSServices(ServiceConfiguration.GetApiKey());

            DeliveryListModel model = new DeliveryListModel();

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                model.Page = 1;
                idpagination = null;
            }
            else
            {
                model.Page = page;
            }

            PaginationDelivery result = new PaginationDelivery();
            try
            {
                result = servicio.ListDeliveries(page, name, tipo, operador, antboxs, fecha_solicitud, fecha_entrega, horario, status, idpagination);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationDelivery();
            }

            model.Deliveries = result;
            model.Name = name;
            model.Tipo = tipo;
            model.Operador = operador;
            model.Antboxs = antboxs;
            model.Solicitud = fecha_solicitud;
            model.Entrega = fecha_entrega;
            model.Horario = horario;

            return View(model);
        }

        public ActionResult Recolecciones(string name, string tipo, string operador, string antboxs, string fecha_solicitud, string fecha_recoleccion, string horario, string status, int? page, string idpagination, string vp)
        {
            var servicio = new CSServices(ServiceConfiguration.GetApiKey());

            PickupListModel model = new PickupListModel();
            PaginationPickup result = new PaginationPickup();

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                idpagination = null;
                model.Page = 1;
            }
            else
            {
                model.Page = page;
            }

            try
            {
                result = servicio.ListPickups(page, name, tipo, operador, antboxs, fecha_solicitud, fecha_recoleccion, horario, status, idpagination);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationPickup();
            }

            model.Pickups = result;
            model.Name = name;
            model.Tipo = tipo;
            model.Operador = operador;
            model.Antboxs = antboxs;
            model.Solicitud = fecha_solicitud;
            model.Recoleccion = fecha_recoleccion;
            model.Horario = horario;

            return View(model);
        }

        public ActionResult Logout()
        {
            Session["helpdesk"] = null;
            return RedirectToAction("CustomerService", "Login");
        }

        public JsonResult GetCustomer(string idCustomer)
        {
            var service = new CustomerServices(ServiceConfiguration.GetApiKey());

            var result = service.SearchCustomer(idCustomer);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAddress(string idAddress)
        {
            var service = new AddressService(ServiceConfiguration.GetApiKey());

            var result = service.SearchAddress(idAddress);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPayments(string idCustomer)
        {
            var service = new PaymentService(ServiceConfiguration.GetApiKey());

            var result = service.ListCharges(idCustomer);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAntboxs(string idCustomer)
        {
            var servicio = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            PaginationAntBoxes result = new PaginationAntBoxes();
            try
            {
                result = servicio.ListAntBoxes(idCustomer, AntBoxStatusEnum.Defualt, 1);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationAntBoxes();
            }
            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistorial(string idCustomer)
        {
            var servicio = new TaskService(ServiceConfiguration.GetApiKey());

            PaginationCustomerTask result = new PaginationCustomerTask();
            try
            {
                result = servicio.ListTaskByCustomer(idCustomer, 1);
            }
            catch (Exception ex)
            {
            }

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAntboxsTasks(string idCustomer, string folio, string type)
        {
            var servicio = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            PaginationAntBoxes result = new PaginationAntBoxes();
            try
            {
                result = servicio.ListAntBoxesTasks(idCustomer, folio, AntBoxStatusEnum.Defualt, 1);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationAntBoxes();
            }
            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }
	}
    public class RedirectingActionHelpDeskAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Session.Contents["helpdesk"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Login",
                    action = "CustomerService"
                }));
            }
        }
    }
}