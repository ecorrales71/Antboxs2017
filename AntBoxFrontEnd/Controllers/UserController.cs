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
        public ActionResult CreateUser(UserRequestOptions modelUser)
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

        public JsonResult UpdateUser(string id, string name, string lastname, string lastname2, string mobile_phone, string profile, bool change_password)
        {
            try
            {
                bool change_passwordv;

                change_passwordv = change_password;

                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                UserUpdateOptions up = new UserUpdateOptions
                {
                    Name = name,
                    Lastname = lastname,
                    Lastname2 = lastname2,
                    Mobile_phone = mobile_phone,
                    Profile = profile,
                    Change_password = change_passwordv
                };

                var userService = new UserServices(ServiceConfiguration.GetApiKey());

                var result = userService.UpdateUser(up, id);

                return Json(new { success = result, responseText = "Usuario actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL USUARIO" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult DeleteUser(string id)
        {
            var userService = new UserServices(ServiceConfiguration.GetApiKey());

            var result = userService.DeleteUser(id);


            if(result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUser(string id)
        {
            var userService = new UserServices(ServiceConfiguration.GetApiKey());

            var result = userService.SearchUser(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }


}