using AntBoxFrontEnd.Code;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Coupon;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
using AntBoxFrontEnd.Services.Listing;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.User;
using AntBoxFrontEnd.Services.Zipcodes;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        public ActionResult Catalogos(string status)
        {

            var servicio = new BoxesService(ServiceConfiguration.GetApiKey());
            var servicioUser = new UserServices(ServiceConfiguration.GetApiKey());

            PaginationBoxesResponse result = new PaginationBoxesResponse();
            PaginationUser resultUsers = new PaginationUser();
            var statusValue = StatusBoxes.Active;
            try
            {
                resultUsers = servicioUser.ListUsersPagination(1);
                
                if (!string.IsNullOrEmpty(status))
                {
                    statusValue = status;
                }
                result = servicio.ListBoxesPagination(statusValue, null, 1, null, "size,label,price,secure_label,secure,status,activation_date,slu");
            }
            catch (Exception ex)
            {
            }

            BoxModel boxresponse = new BoxModel();
            boxresponse.Boxes = result;
            boxresponse.Users = resultUsers.Users;
            boxresponse.Status = statusValue;

            return View(boxresponse);
        }

        public ActionResult Codigos(string codigo, string estado, string municipio, string colonia, string registro, string status, int? page, string idpagination, string vp)
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

            PaginationZipCodesResponse result = new PaginationZipCodesResponse();
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

        public ActionResult Reportes()
        {
            return View();
        }

        public ActionResult ReporteUsuarios(int? page, string from, string to, string status, string idpagination, string vp)
        {
            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            ReporteUsuariosModel model = new ReporteUsuariosModel();

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

            PaginationCustomerResponse result = new PaginationCustomerResponse();
            try
            {
                result = servicio.ListCustomer(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            model.Usuarios = result;
            model.From = from;
            model.To = to;
            model.Status = status;

            return View(model);
        }

        public ActionResult ReportePagos(int? page, string from, string to, string type, string idpagination, string vp)
        {
            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            ReportePagosModel model = new ReportePagosModel();

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

            PaginationPayments result = new PaginationPayments();
            try
            {
                result = servicio.ListPayments(page, from, to, type, idpagination);
            }
            catch (Exception ex)
            {
            }

            model.Pagos = result;
            model.From = from;
            model.To = to;
            model.Type = type;

            return View(model);
        }

        public ActionResult ReporteEntregas(int? page, string from, string to, string status, string idpagination, string vp)
        {
            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            ReporteDeliveriesModel model = new ReporteDeliveriesModel();

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
                result = servicio.ListDeliveries(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            model.Deliveries = result;
            model.From = from;
            model.To = to;
            model.Status = status;

            return View(model);
        }

        public ActionResult ReporteRecolecciones(int? page, string from, string to, string status, string idpagination, string vp)
        {
            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            ReportePickupsModel model = new ReportePickupsModel();

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

            PaginationPickup result = new PaginationPickup();
            try
            {
                result = servicio.ListPickups(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            model.Pickups = result;
            model.From = from;
            model.To = to;
            model.Status = status;

            return View(model);
        }

        [HttpGet]
        public FileContentResult ExportToExcelUsers(int? page, string from, string to, string status, string idpagination, string vp)
        {

            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                idpagination = null;
            }

            PaginationCustomerResponse result = new PaginationCustomerResponse();
            try
            {
                result = servicio.ListCustomer(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            List<CustomerListingResponse> technologies;
            if (result != null)
            {
                technologies = result.Customers;
            }
            else
            {
                technologies = new List<CustomerListingResponse>();
            }

            var columns1 = new Dictionary<string, string>();
            columns1.Add("Name", "Nombre");
            columns1.Add("Lastname", "A. Paterno");
            columns1.Add("Lastname2v", "A. Materno");
            columns1.Add("Email", "Email");
            columns1.Add("MiembroDesde", "Miembro desde");
            columns1.Add("Antboxsnumber", "Numero de Antboxs");
            columns1.Add("Activo", "Activo");

            byte[] filecontent = ExcelExportHelper.ExportExcel(technologies, "Clientes", false, columns1);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "Clientes.xlsx");
        }

        [HttpGet]
        public FileContentResult ExportToExcelPayments(int? page, string from, string to, string status, string idpagination, string vp)
        {

            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                idpagination = null;
            }

            PaginationPayments result = new PaginationPayments();
            try
            {
                result = servicio.ListPayments(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            List<PaymentObject> payments;
            if (result != null)
            {
                payments = result.Payments;
            }
            else
            {
                payments = new List<PaymentObject>();
            }

            var columns1 = new Dictionary<string, string>();
            columns1.Add("Name", "Nombre");
            columns1.Add("Lastname", "A. Paterno");
            columns1.Add("Lastname2v", "A. Materno");
            columns1.Add("Email", "Email");
            columns1.Add("Rfc", "RFC");
            columns1.Add("Amount", "Monto");

            byte[] filecontent = ExcelExportHelper.ExportExcel(payments, "Pagos", false, columns1);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "Pagos.xlsx");
        }

        [HttpGet]
        public FileContentResult ExportToExcelPickups(int? page, string from, string to, string status, string idpagination, string vp)
        {

            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                idpagination = null;
            }

            PaginationPickup result = new PaginationPickup();
            try
            {
                result = servicio.ListPickups(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            List<PickupResponse> payments;
            if (result != null)
            {
                payments = result.Pickups;
            }
            else
            {
                payments = new List<PickupResponse>();
            }

            var columns1 = new Dictionary<string, string>();
            columns1.Add("Name", "Nombre");
            columns1.Add("Lastname", "A. Paterno");
            columns1.Add("Lastname2v", "A. Materno");
            columns1.Add("Email", "Email");
            columns1.Add("Antboxs", "Antboxs");
            columns1.Add("Solicitud", "Solicitud");
            columns1.Add("Recoleccion", "Recoleccion");
            columns1.Add("Time", "Horario");
            columns1.Add("Estatus", "Estatus");

            byte[] filecontent = ExcelExportHelper.ExportExcel(payments, "Recolecciones", false, columns1);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "Recolecciones.xlsx");
        }

        [HttpGet]
        public FileContentResult ExportToExcelDeliveries(int? page, string from, string to, string status, string idpagination, string vp)
        {

            var servicio = new ListingServices(ServiceConfiguration.GetApiKey());

            if (string.IsNullOrEmpty(vp))
            {
                page = 1;
                idpagination = null;
            }

            PaginationDelivery result = new PaginationDelivery();
            try
            {
                result = servicio.ListDeliveries(page, from, to, status, idpagination);
            }
            catch (Exception ex)
            {
            }

            List<DeliveryResponse> payments;
            if (result != null)
            {
                payments = result.Deliveries;
            }
            else
            {
                payments = new List<DeliveryResponse>();
            }

            var columns1 = new Dictionary<string, string>();
            columns1.Add("Name", "Nombre");
            columns1.Add("Lastname", "A. Paterno");
            columns1.Add("Lastname2v", "A. Materno");
            columns1.Add("Email", "Email");
            columns1.Add("Antboxs", "Antboxs");
            columns1.Add("Solicitud", "Solicitud");
            columns1.Add("Entrega", "Entrega");
            columns1.Add("Time", "Horario");
            columns1.Add("Estatus", "Estatus");

            byte[] filecontent = ExcelExportHelper.ExportExcel(payments, "Entregas", false, columns1);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "Entregas.xlsx");
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