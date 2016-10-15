using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class CodeController : Controller
    {
        //
        // GET: /Code/
        public ActionResult CreateCode(CodeRequestOptions modelCode)
        {
            try
            {

                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                var ser = new CodeService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateCode(modelCode);
                return Json(new { success = true, responseText = "Codigo registrado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL CÓDIGO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateCode(string id, string code, int quantity, decimal amount, string from, string to, string created_by)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                CodeUpdateOptions cu = new CodeUpdateOptions
                {
                    Code = code,
                    Quantity = quantity,
                    Amount = amount,
                    From = from,
                    To = to,
                    Created_by = created_by
                };

                var couponService = new CodeService(ServiceConfiguration.GetApiKey());

                var result = couponService.UpdateCode(cu, id);

                return Json(new { success = result, responseText = "Cupón actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL CUPÓN" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult DeleteCode(string id)
        {
            var couponService = new CodeService(ServiceConfiguration.GetApiKey());

            var result = couponService.DeleteCode(id);


            if (result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCode(string id)
        {
            var couponService = new CodeService(ServiceConfiguration.GetApiKey());

            var result = couponService.SearchCode(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}
