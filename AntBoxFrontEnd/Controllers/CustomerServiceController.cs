using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Customer;
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
                result = servicio.ListCustomer(1);
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

        public ActionResult Entregas()
        {
            return View();
        }

        public ActionResult Recolecciones()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["helpdesk"] = null;
            return RedirectToAction("Index", "Login");
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
                    action = "Index"
                }));
            }
        }
    }
}