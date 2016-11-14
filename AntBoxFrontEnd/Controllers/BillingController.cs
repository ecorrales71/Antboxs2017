using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Billing;
using AntBoxFrontEnd.Services.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class BillingController : Controller
    {
        //
        // GET: /BillingAddress/
        public ActionResult CreateBilling(string idpago, string direccion)
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                BillingRequestOptions model = new BillingRequestOptions
                {
                    Billing_address_id = direccion,
                    Payment_id = idpago
                };

                var ser = new BillingService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateBilling(model);
                return Json(new { success = res, responseText = "Facturacion creada" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL CREAR LA FACTURACION" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateBilling(string idpago, string direccion)
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                BillingUpdateOptions cu = new BillingUpdateOptions
                {
                    Billing_address_id = direccion,
                    Payment_id = idpago
                };

                var service = new BillingService(ServiceConfiguration.GetApiKey());

                var result = service.UpdateBilling(cu);

                return Json(new { success = result, responseText = "Factura actualizada" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR LA FACTURA" }, JsonRequestBehavior.AllowGet);
            }


        }
	}
}
