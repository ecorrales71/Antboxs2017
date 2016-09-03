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



        // GET: Precio
        public ActionResult Index()
        {

            var model = new PriceViewModel().GetPriceViewModel();


            return View("Precios", model);
        }


        //[HttpPost]
        //public ActionResult Index(PriceViewModel model)
        //{
        //    if (model == null)
        //        model = new PriceViewModel().GetPriceViewModel();


        //    return View("Precios", model);
        //}


        [HttpPost]
        public ActionResult Calculate(PriceViewModel modelPrice)
        {

            var subTotal = modelPrice.LineTotal;

            var pDisccount = this.GetDisccount(modelPrice.DisccountCode);



            var disccount = subTotal * pDisccount;
            modelPrice.LineDiscount = disccount;


            var lineSubtotal = subTotal - disccount;
            modelPrice.LineSubtotal = lineSubtotal;

            int iva;
            var isFeeOk = int.TryParse(WebConfigurationManager.AppSettings["Iva"], out iva);

            decimal fee = 0;

            if (isFeeOk)
                fee = lineSubtotal * (iva / 100);

            modelPrice.LineFee = fee;


            var grandTotal = lineSubtotal + fee;
            modelPrice.GrandTotal = grandTotal;

            Session["AntBoxes"] = modelPrice;


            return PartialView("PricePartialView", modelPrice);

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