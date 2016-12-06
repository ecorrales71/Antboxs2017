using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class TasksController : Controller
    {
        public JsonResult CreateTask(string idcustomer, string idAddress, DateTime dFrom, DateTime dTo)
        {
            var t = new TaskRequestOption
            {
                Address_id = idAddress
                ,
                customer_id = idcustomer
                ,
                from = DateHelpers.ToUnixTime(dFrom)
                ,
                To = DateHelpers.ToUnixTime(dTo)
            };

            var ts = new TaskService(ServiceConfiguration.GetApiKey());

            var result = ts.CreateTask(t);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTask(string idTask, string state)
        {
            var t = new TaskUpdateOptions
            {
                State = state
            };

            var ts = new TaskService(ServiceConfiguration.GetApiKey());

            var result = ts.UpdateTask(t, idTask);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListTask(string idWorker)
        {
            var ts = new TaskService(ServiceConfiguration.GetApiKey());

            var result = ts.ListTaskByWorker(idWorker);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}