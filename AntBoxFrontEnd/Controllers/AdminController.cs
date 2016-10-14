using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AntBoxFrontEnd.Controllers
{
    [RedirectingAction]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var servicio = new UserServices(ServiceConfiguration.GetApiKey());

            PaginationUser result = new PaginationUser();
            try
            {
                result = servicio.ListUsersPagination(1);
            }
            catch (Exception ex)
            {
            }

            return View(result);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Catalogos()
        {
            return View();
        }

        public ActionResult Codigos()
        {
            return View();
        }

        public ActionResult Cupones()
        {
            return View();
        }

        public ActionResult Cuenta()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
    public class RedirectingActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.Session.Contents["admin"] == null)
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