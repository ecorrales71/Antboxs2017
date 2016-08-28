using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Infrastructure;

namespace AntBoxFrontEnd.Controllers
{
    public class BoxController : Controller
    {
        //// POST: Box
        //public JsonResult CreateBox()
        //{

        //    var cus = new BoxesRequestOptions
        //    {
        //        Label = "ALIAS " + i,
        //        Model = "modelo " + i,
        //        Price = 300,
        //        Secure = 1500,
        //        Registered_by = id,
        //        Size = "Size " + i,
        //        Status = true
        //    };

        //    var ser = new BoxesService(ServiceConfiguration.GetApiKey());

        //    var result = ser.CreateBoxes(cus);

        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}
    }
}