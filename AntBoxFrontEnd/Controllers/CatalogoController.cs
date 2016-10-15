using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Boxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    [RedirectingAction]
    public class CatalogoController : Controller
    {
        public ActionResult CreateBox(BoxesRequestOptions modelBox)
        {
            try
            {

                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                var ser = new BoxesService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateBoxes(modelBox);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR LA DIRECCIÓN" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateBox(string id, string registered_by, string model, string size, string label, decimal price, string secure_label, decimal secure, bool status, string activation_date, decimal slu)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                BoxesUpdateOptions bu = new BoxesUpdateOptions
                {
                    Registered_by = registered_by,
                    Model = model,
                    Size = size,
                    Label = label,
                    Price = price,
                    Secure_label = secure_label,
                    Secure = secure,
                    Activation_date = activation_date,
                    Slu = slu
                };

                var boxService = new BoxesService(ServiceConfiguration.GetApiKey());

                var result = boxService.UpdateBox(bu, id);

                return Json(new { success = result, responseText = "Box actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL BOX" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult DeleteBox(string id)
        {
            var boxService = new BoxesService(ServiceConfiguration.GetApiKey());

            var result = boxService.DeleteBoxes(id);


            if(result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBox(string id)
        {
            var boxService = new BoxesService(ServiceConfiguration.GetApiKey());

            var result = boxService.SearchBox(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}