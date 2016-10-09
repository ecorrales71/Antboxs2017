using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class AntBoxController : Controller
    {
        [HttpPost]
        public ActionResult UpdateAjax(string id, string name, string description)
        {
            var abs = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            AntBoxUpdateRequest updateOptions = new AntBoxUpdateRequest()
            {
                name = name,
                Description = description
            };
            bool result = false;
            try
            {
                result = abs.UpdateAntBox(updateOptions, id);
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PaginationAjax(string idPagination, int page)
        {
            var servicio = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            PaginationAntBoxes result = new PaginationAntBoxes();
            try
            {
                result = servicio.ListAntBoxes(((CustomerResponse)Session["customer"]).Id, AntBoxStatusEnum.Defualt, page, idPagination);
            }
            catch (Exception ex)
            {
                
            }
            if (result == null)
            {
                result = new PaginationAntBoxes();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}