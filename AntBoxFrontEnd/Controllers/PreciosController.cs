using AntBoxFrontEnd.Models;
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





        public decimal GetDisccount(string codgo)
        {


            return Convert.ToDecimal(0.00);
        }


        


    }
}