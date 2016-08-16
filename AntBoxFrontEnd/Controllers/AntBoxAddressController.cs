using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class AntBoxAddressController : Controller
    {
        // GET: AntBoxAddress
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAddress(string customerId)
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());
            var res = l.ListAddresses(customerId);
            
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NewAddress(string customerId)
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());
            var res = l.ListAddresses(customerId);

            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}