using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult CreateClient(ClientRequestOptions modelCoupon)
        {
            try
            {
                var ser = new ClientService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateClient(modelCoupon);
                return Json(new { success = true, responseText = "CUPÓN REGISTRADO CORRECTAMENTE" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL CUPÓN" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}