using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    [RedirectingAction]
    public class WorkerController : Controller
    {
        // GET: Worker
        public ActionResult CreateWorker(WorkerRequestOption modelWorker)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                var ser = new WorkerService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateWorker(modelWorker);
                
                return Json(new { success = true, responseText = "USUARIO AGREGADO CORRECTAMENTE" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL USUARIO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateWorker(string id, string name, bool status)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                WorkerUpdateOptions up = new WorkerUpdateOptions
                {
                    Name = name,
                    Status = status
                };

                var userService = new WorkerService(ServiceConfiguration.GetApiKey());

                var result = userService.UpdateWorker(up, id);

                return Json(new { success = result, responseText = "Usuario actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL USUARIO" }, JsonRequestBehavior.AllowGet);
            }


        }

        /*public JsonResult DeleteWorker(string id)
        {
            var userService = new WorkerService(ServiceConfiguration.GetApiKey());

            var result = userService.DeleteWorker(id);


            if(result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }*/

        public JsonResult GetWorker(string id)
        {
            var userService = new WorkerService(ServiceConfiguration.GetApiKey());

            var result = userService.SearchWorker(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /*[HttpPost]
        public ActionResult PaginationAjax(string idPagination, int page)
        {
            var servicio = new WorkerService(ServiceConfiguration.GetApiKey());

            PaginationWorker result = new PaginationWorker();
            try
            {
                result = servicio.ListWorkersPagination(page, idPagination);
            }
            catch (Exception ex)
            {

            }
            if (result == null)
            {
                result = new PaginationWorker();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }*/
    }


}