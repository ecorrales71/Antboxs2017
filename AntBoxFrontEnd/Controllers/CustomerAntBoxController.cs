using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class CustomerAntBoxController : Controller
    {

        private JsonResult CreateCustomer(CustomerRequestOptions customer)
        {
            var service = new CustomerServices(ServiceConfiguration.GetApiKey());

            var result = service.CreateCustomer(customer);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private JsonResult UpdateCustomer(string idCustomer, CustomerUpdateOptions customer)
        {
            var service = new CustomerServices(ServiceConfiguration.GetApiKey());

            var result = service.UpdateCustomer(customer, idCustomer);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        private JsonResult GetCustomer(string idCustomer)
        {
            var service = new CustomerServices(ServiceConfiguration.GetApiKey());

            var result = service.SearchCustomer(idCustomer);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private JsonResult Login(LoginCreateOptions login)
        {
            LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

            string id = ls.HovaLogin(login);

            return GetCustomer(id);

        }


    }
}