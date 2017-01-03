using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        public ActionResult UpdateCardAjax(string id, string status)
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            PaymentRequestOptions updateOptions = new PaymentRequestOptions()
            {
                State=status
            };
            bool result = false;
            try
            {
                result = ps.UpdatePayment(updateOptions, id);
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteCardAjax(string id)
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());
            
            bool result = false;
            try
            {
                result = ps.DeletePayment(id);
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetCardsAjax()
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            List < CardObject > result = new List<CardObject>();
            try
            {
                result = ps.ListPaymetCards(((CustomerResponse)Session["customer"]).Id);

                return PartialView("_CustomerCards",result);
            } catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult GetCardsAjaxJson()
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            List<CardObject> result = new List<CardObject>();
            try
            {
                result = ps.ListPaymetCards(((CustomerResponse)Session["customer"]).Id);

                return Json(new { success = true, cards = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult RegisterCardAjax(string deviceId, string state, string token)
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
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult MakePaymentAjax(decimal amount)
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());
            Charge c = new Charge()
            {
                Amount = amount,
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

        [HttpPost]
        public ActionResult Recibo(string folio, string taskId)
        {
            //var servicioTask = new TaskService(ServiceConfiguration.GetApiKey());
            var servicioAntBox = new AntBoxesServices(ServiceConfiguration.GetApiKey());
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            PaginationAntBoxes result = new PaginationAntBoxes();
            List<PaymentDetailResponse> payments = new List<PaymentDetailResponse>();
            try
            {
                result = servicioAntBox.ListAntBoxesTasks(((CustomerResponse)Session["customer"]).Id, folio, AntBoxStatusEnum.Defualt, 1);
                payments = ps.PaymentDetail(((CustomerResponse)Session["customer"]).Id, folio, taskId);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, antbox = result, payments = payments }, JsonRequestBehavior.AllowGet);

        }



    }
}