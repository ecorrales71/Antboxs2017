using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class MovimientosController : Controller
    {
        [HttpPost]
        public ActionResult PaginationAjax(string idPagination, int page)
        {
            var servicio = new TaskService(ServiceConfiguration.GetApiKey());

            PaginationCustomerTask result = new PaginationCustomerTask();
            try
            {
                result = servicio.ListTaskByCustomer(((CustomerResponse)Session["customer"]).Id, page, idPagination);
            }
            catch (Exception ex)
            {

            }
            if (result == null)
            {
                result = new PaginationCustomerTask();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}