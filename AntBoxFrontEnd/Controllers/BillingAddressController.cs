using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.BillingAddress;
using AntBoxFrontEnd.Services.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class BillingAddressController : Controller
    {
        //
        // GET: /BillingAddress/
        public ActionResult CreateBillingAddress(BillingAddressRequestOptions modelBillingAddress)
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                modelBillingAddress.Customer_id = ((CustomerResponse)Session["customer"]).Id;

                var ser = new BillingAddressService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateBillingAddress(modelBillingAddress);
                bool verif = true;
                if (res == null)
                {
                    verif = false;
                }
                return Json(new { success = verif, objectaddress = res, responseText = "Direccion de facturación registrada correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR LA DIRECCIÓN DE FACTURACION" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateBillingAddress(string id, string alias, string razonsocial, string rfc, string referencias, string Street, string External_number, string Internal_number, string Zipcode, string Delegation, string Neighborhood, string City, string State, string Country, string Billing_email)
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                BillingAddressUpdateOptions cu = new BillingAddressUpdateOptions
                {
                    Bussiness_name = razonsocial,
                    Alias = alias,
                    Rfc_id = rfc,
                    References = referencias,
                    Street = Street,
                    External_number = External_number,
                    Internal_number = Internal_number,
                    Zipcode = Zipcode,
                    Delegation = Delegation,
                    Neighborhood = Neighborhood,
                    City = City,
                    State = State,
                    Country = Country,
                    Billing_email = Billing_email
                };

                var couponService = new BillingAddressService(ServiceConfiguration.GetApiKey());

                var result = couponService.UpdateBillingAddress(cu, id);

                return Json(new { success = result, responseText = "Cupón actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR LA DIRECCIÓN DE FACTURACIÓN" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult DeleteBillingAddress(string id)
        {
            var couponService = new BillingAddressService(ServiceConfiguration.GetApiKey());

            var result = couponService.DeleteBillingAddress(id);


            if (result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBillingAddress(string id)
        {
            var couponService = new BillingAddressService(ServiceConfiguration.GetApiKey());

            var result = couponService.SearchBillingAddress(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaginationAjax(string idCustomer, string idPagination, int page)
        {
            var servicio = new BillingAddressService(ServiceConfiguration.GetApiKey());

            List<BillingAddressResponse> result = new List<BillingAddressResponse>();
            try
            {
                result = servicio.ListBillingAddresses(idCustomer, page, idPagination);
            }
            catch (Exception ex)
            {

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
	}
}
