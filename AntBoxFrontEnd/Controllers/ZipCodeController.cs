using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Zipcodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class ZipCodeController : Controller
    {
        //
        // GET: /Code/
        public ActionResult CreateZipCode(ZipCodeRequestOptions modelCode)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                var ser = new ZipcodeService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateZipCode(modelCode);
                return Json(new { success = res, responseText = "Codigo zip registrado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL CÓDIGO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateZipCode(string id, string country, string state, string neighborhood, string delegation)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                ZipCodeUpdateOptions cu = new ZipCodeUpdateOptions
                {
                    Country = country,
                    State = state,
                    Neighborhood = neighborhood,
                    Delegation = delegation,
                };

                var couponService = new ZipcodeService(ServiceConfiguration.GetApiKey());

                var result = couponService.UpdateZipCode(cu, id);

                return Json(new { success = result, responseText = "Código zip actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL CÓDIGO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteCode(string id)
        {
            var couponService = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = couponService.DeleteZipCode(id);


            if (result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCode(string id)
        {
            var couponService = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = couponService.SearchZipCode(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaginationAjax(string idPagination, int page)
        {
            var servicio = new ZipcodeService(ServiceConfiguration.GetApiKey());

            List<ZipCodeResponse> result = new List<ZipCodeResponse>();
            try
            {
                result = servicio.ListZipCode(page, idPagination);
            }
            catch (Exception ex)
            {

            }
            if (result == null)
            {
                result = new List<ZipCodeResponse>();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}
