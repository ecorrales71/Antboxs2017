using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
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
        public ActionResult Index(string name, string email, string rfc, int? antboxs, string status)
        {
            var servicio = new CustomerServices(ServiceConfiguration.GetApiKey());

            CustomerListModel model = new CustomerListModel();
            PaginationCustomerResponse result = new PaginationCustomerResponse();
            try
            {
                result = servicio.ListCustomer(1, name, email, rfc, antboxs, status);
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

        public ActionResult Antboxs()
        {
            return View();
        }

        public ActionResult Entregas(string name, string tipo, string operador, string antboxs, string fecha_solicitud, string fecha_recoleccion, string horario, string status)
        {
            var servicio = new CSServices(ServiceConfiguration.GetApiKey());

            DeliveryListModel model = new DeliveryListModel();
            PaginationDelivery result = new PaginationDelivery();
            try
            {
                result = servicio.ListDeliveries(1, name, tipo, operador, antboxs, fecha_solicitud, fecha_recoleccion, horario, status);
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
            model.Recoleccion = fecha_recoleccion;
            model.Horario = horario;

            return View(model);
        }

        public ActionResult Recolecciones(string name, string tipo, string operador, string antboxs, string fecha_solicitud, string fecha_recoleccion, string horario, string status)
        {
            var servicio = new CSServices(ServiceConfiguration.GetApiKey());

            PickupListModel model = new PickupListModel();
            PaginationPickup result = new PaginationPickup();
            try
            {
                result = servicio.ListPickups(1, name, tipo, operador, antboxs, fecha_solicitud, fecha_recoleccion, horario, status);
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