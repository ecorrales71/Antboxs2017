using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class CouponController : Controller
    {
        //
        // GET: /Coupon/
        public ActionResult CreateCoupon(CouponRequestOptions modelCoupon)
        {
            try
            {

                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                var ser = new CouponService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateCoupon(modelCoupon);
                return Json(new { success = true , responseText = "CUPÓN REGISTRADO CORRECTAMENTE" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL CUPÓN" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateCoupon(string id, string name, decimal discount)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                CouponUpdateOptions cu = new CouponUpdateOptions
                {
                    Name = name,
                    Discount = discount
                };

                var couponService = new CouponService(ServiceConfiguration.GetApiKey());

                var result = couponService.UpdateCoupon(cu, id);

                return Json(new { success = result, responseText = "Cupón actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL CUPÓN" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult DeleteCoupon(string id)
        {
            var couponService = new CouponService(ServiceConfiguration.GetApiKey());

            var result = couponService.DeleteCoupon(id);


            if (result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCoupon(string id)
        {
            var couponService = new CouponService(ServiceConfiguration.GetApiKey());

            var result = couponService.SearchCoupon(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}