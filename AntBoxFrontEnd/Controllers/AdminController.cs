using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Coupon;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.User;
using AntBoxFrontEnd.Services.Zipcodes;
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

            var servicio = new BoxesService(ServiceConfiguration.GetApiKey());
            var servicioUser = new UserServices(ServiceConfiguration.GetApiKey());

            PaginationBoxesResponse result = new PaginationBoxesResponse();
            PaginationUser resultUsers = new PaginationUser();
            try
            {
                resultUsers = servicioUser.ListUsersPagination(1);
                result = servicio.ListBoxesPagination(StatusBoxes.All, null, 1, null, "size,label,price,secure_label,secure,status,activation_date,slu");
            }
            catch (Exception ex)
            {
            }

            BoxModel boxresponse = new BoxModel();
            boxresponse.Boxes = result;
            boxresponse.Users = resultUsers.Users;

            return View(boxresponse);
        }

        public ActionResult Codigos(string codigo, string estado, string municipio, string colonia, string registro, bool? status, int? page, string idpagination, string vp)
        {

            var servicio = new ZipcodeService(ServiceConfiguration.GetApiKey());

            ZipCodeModel model = new ZipCodeModel();

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

            List<ZipCodeResponse> result = new List<ZipCodeResponse>();
            try
            {
                result = servicio.ListZipCode(page, codigo, estado, municipio, colonia, registro, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            model.Zipcodes = result;
            model.Codigo = codigo;
            model.Estado = estado;
            model.Municipio = municipio;
            model.Colonia = colonia;
            model.Registro = registro;
            model.Status = status;

            return View(model);
        }

        public ActionResult Cupones()
        {
            var servicio = new CouponService(ServiceConfiguration.GetApiKey());
            var servicioCode = new CodeService(ServiceConfiguration.GetApiKey());
            var servicioUser = new UserServices(ServiceConfiguration.GetApiKey());

            PaginationCouponsResponse result = new PaginationCouponsResponse();
            PaginationCodesResponse resultCode = new PaginationCodesResponse();
            PaginationUser resultUsers = new PaginationUser();
            try
            {
                resultUsers = servicioUser.ListUsersPagination(1);
                resultCode = servicioCode.ListCode(1);
                result = servicio.ListCoupon(1);
            }
            catch (Exception ex)
            {
            }

            CouponModel response = new CouponModel();
            response.Coupons = result;
            response.Codes = resultCode;
            response.Users = resultUsers.Users;

            return View(response);
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