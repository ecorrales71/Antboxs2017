using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    [RedirectingAction]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult RegisterUser(UserRequestOptions modelUser)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                var ser = new UserServices(ServiceConfiguration.GetApiKey());
                var res = ser.CreateUser(modelUser);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR LA DIRECCIÓN" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}