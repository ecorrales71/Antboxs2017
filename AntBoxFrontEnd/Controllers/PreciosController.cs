using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class PreciosController : Controller
    {




        [HttpPost]
        public ActionResult Index(/*PriceViewModel model*/)
        {
            //if (model == null)
            //    model = new PriceViewModel().GetPriceViewModel();


            return View("Precios"/*, model*/);
        }




        [HttpPost]
        public ActionResult MakePaymentAjax(string deviceId, string state, string token)
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            PaymentRequestOptions pro = new PaymentRequestOptions
            {
                Customer_id = ((CustomerResponse)Session["customer"]).Id,
                Device_id = deviceId,
                State = state,
                Token = token
            };
            bool result = ps.CreatePaymentCard(pro);
            if (result)
            {
                Charge c = new Charge()
                {
                    Amount = 200,
                    Customer_id = ((CustomerResponse)Session["customer"]).Id
                };
                if (ps.DoCharge(c))
                {
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        public decimal GetDisccount(string codgo)
        {


            return Convert.ToDecimal(0.00);
        }


        
        

    }
}