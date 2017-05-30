using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Repository;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Services.Contact;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Zipcodes;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var serAntboxs = new AntBoxesServices(ServiceConfiguration.GetApiKey());
            var serCustomer = new CustomerServices(ServiceConfiguration.GetApiKey());

            var antboxs = serAntboxs.CountingAntboxs();
            var customers = serCustomer.CountingCustomers();

            var model = new HomeViewModel();
            model.antboxs = antboxs + 1000;
            model.customers = customers + 250;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de.";

            return View();
        }

        public ActionResult ComoFunciona()
        {
            ViewBag.Message = "¿Cómo Funciona?";

            return View();
        }

        public ActionResult QueDeboHacer()
        {
            ViewBag.Message = "¿Qué debo hacer?";

            return View();
        }

        public ActionResult Precios()
        {
            ViewBag.Message = "Precios";

            return View();
        }

        public ActionResult Contacto()
        {
            ViewBag.Message = "Pagina de contacto.";

            return View();
        }

        public JsonResult ContactPost(ContactRequestOptions modelContact)
        {
            try
            {
                var ser = new ContactService(ServiceConfiguration.GetApiKey());
                var res = ser.Send(modelContact);
                return Json(new { success = res, responseText = "Mensaje enviado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "Ocurrio un error al enviar el mensaje, intentelo de nuevo mas tarde" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PreguntasFrecuentes()
        {
            ViewBag.Message = "Preguntas frecuentes.";

            return View();
        }


        public ActionResult AgendarEntregas()
        {
            ViewBag.Message = "Agendar entregas.";

            List<SelectListItem> delegaciones = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "Selecciona una delegacion" } };
            List<SelectListItem> colonias = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "Selecciona una colonia" } };

            ViewBag.DelegationList = new SelectList(delegaciones, "Value", "Text");
            ViewBag.ColoniasList = new SelectList(colonias, "Value", "Text");

            return View();
        }

        public ActionResult CrearCuenta(AgendTaskModel modelagend)
        {
            if (modelagend.Paso != "2")
            {
                return RedirectToAction("Index", "Precios");
            }

            if (modelagend.Zipcode != null)
            {
                Session["TasksTemp"] = modelagend;
            }
            ViewBag.Message = "Agendar entregas.";

            return View();
        }

        public ActionResult OrdenCompleta()
        {
            ViewBag.Message = "Agendar entregas.";

            return View();
        }


        #region Address
        [HttpGet]
        public ActionResult CreateAddress()
        {
            var vm = new AntBoxAddressViewModel();

            return PartialView(vm);

        }


        public ActionResult GuardaTempTasks(AgendTaskModel modelagend)
        {
            //guardamos de forma temporal las direcciones y tareas en la session
            Session["TasksTemp"] = modelagend;

            return View("CrearCuenta");
        }


        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{

        //    var addres = new AddressRequestOptions
        //    {
        //        Customer_id = Convert.ToString(collection["Customer_id"]),

        //        Alias =  Convert.ToString(collection["Alias"]),

        //        Street = Convert.ToString(collection["Street"]),
        //        Internal_number = Convert.ToString(collection["Internal_number"]),
        //        External_number = Convert.ToString(collection["External_number"]),

        //        Zipcode = Convert.ToString(collection["Zipcode"]),
        //        Delegation = Convert.ToString(collection["Delegation"]),

        //        City = Convert.ToString(collection["City"]),
        //        Country = Convert.ToString(collection["Country"]),

        //        Neighborhood = Convert.ToString(collection["Neighborhood"]),
        //        References = Convert.ToString(collection["References"]),
        //        State = Convert.ToString(collection["State"]),

        //    };
        //    return CreateAddress(addres);  


        //}



        [HttpPost]
        public JsonResult CreateAddress(AntBoxAddressViewModel address)
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                CustomerResponse customer = (CustomerResponse)Session["customer"];

<<<<<<< HEAD
                customer = actualizaStep(3);

                Session["customer"] = customer;
=======

>>>>>>> 92f5bf9412da85054e6afa1da10323ce05fb944c

                var requestOption = Mapper.Map<AntBoxAddressViewModel, AddressRequestOptions>(address);

                var addresService = new AddressService(ServiceConfiguration.GetApiKey());


                requestOption.Customer_id = customer.Id;

                var result = addresService.CreateAddress(requestOption);

                return Json(new { success = result, step = customer.step, responseText = "DIRECCIÓN REGISTRADA" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR LA DIRECCIÓN" }, JsonRequestBehavior.AllowGet);
            }
        }

        public CustomerResponse actualizaStep(short step)
        {
            CustomerResponse customer = ((CustomerResponse)Session["customer"]);
            if (customer.step < step)
            {
                antboxsbdEntities db = new antboxsbdEntities();

                try
                {
                    db.Database.Connection.Open();
                    User obj = db.Users.Where(e => e.email == customer.Email)
                        .FirstOrDefault();

                    if (customer.step < step)
                    {
                        obj.step = step;
                        db.SaveChanges();
                        customer.step = step;
                    }

                }
                catch (Exception ex)
                {

                }

                db.Database.Connection.Close();
            }
            return customer;
        }

        public JsonResult UpdateAddress(AntBoxAddressViewModel address)
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                CustomerResponse customer = (CustomerResponse)Session["customer"];

                var addressService = new AddressService(ServiceConfiguration.GetApiKey());

                var requestOption = Mapper.Map<AntBoxAddressViewModel, AddressUpdateOptions>(address);




                var result = addressService.UpdateAddress(requestOption, address.Id);

                return Json(new { success = result, responseText = "Dirección actualizada" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR LA DIRECCIÓN" }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult ListAddresses(int numPag = 1, string idPag = null)
        {

            if (Session["customer"] == null)
            {
                return null;
            }

            CustomerResponse customer = (CustomerResponse)Session["customer"];

            try
            {
                var addressService = new AddressService(ServiceConfiguration.GetApiKey());

                var result = addressService.ListAddresses(customer.Id, numPag, idPag);

                var antBoxResult = new List<AntBoxAddressViewModel>();

                result.ForEach(r =>
                {
                    var dir = addressService.SearchAddress(r.Id);

                    var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);

                    antBoxResult.Add(map);
                });

                return Json(antBoxResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL LISTAR LAS DIRECCIONES" }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetAddress(string id)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addressService.SearchAddress(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteAdress(string idAddress)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addressService.DeleteAddress(idAddress);


            if (result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ValidateAddress(AddressValidateRequest address)
        {
            try
            {
                var addressService = new AddressService(ServiceConfiguration.GetApiKey());
                var result = addressService.ValidateAddress(address);
                if (result)
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "Direccion no valida" }, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchZip(string zipcode)
        {
            var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = service.SearchZipCode(zipcode);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDelegation(string zipcode)
        {
            var delegations = new List<SelectListItem>();
            if (zipcode == null)
                zipcode = "";

            try
            {
                var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

                var results = service.SearchZipCode(zipcode);

                results.ForEach(x =>
                {
                    delegations.Add(new SelectListItem
                    {
                        Value = x.Delegation,
                        Text = x.Delegation

                    });
                });
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
            }


            return Json(new { result = delegations }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetColonias(string zipcode = null)
        {
            var col = new List<SelectListItem>();

            try
            {
                var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

                var results = service.SearchZipCode(zipcode);

                results.ForEach(x =>
                {
                    col.Add(new SelectListItem
                    {
                        Text = x.Neighborhood,
                        Value = x.Neighborhood
                    });
                });
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
            }



            return Json(new { result = col }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetState(string zipcode = null)
        {

            string state = "";

            var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var results = service.SearchZipCode(zipcode);

            try
            {
                state = results.FirstOrDefault().State;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
            }
            return Json(new { result = state }, JsonRequestBehavior.AllowGet);

        }

        #endregion
    }
}