using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Address;
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
            return View();
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

        public ActionResult PreguntasFrecuentes()
        {
            ViewBag.Message = "Preguntas frecuentes.";

            return View();
        }


        public ActionResult AgendarEntregas()
        {
            ViewBag.Message = "Agendar entregas.";

            List<SelectListItem> delegaciones = new List<SelectListItem>() { new SelectListItem { Value = "",  Text= "Selecciona código postal"} };
            List<SelectListItem> colonias = new List<SelectListItem>() { new SelectListItem { Value = "", Text = "Selecciona código postal" } };

            ViewBag.DelegationList = new SelectList(delegaciones, "Value", "Text");
            ViewBag.ColoniasList = new SelectList(delegaciones, "Value", "Text");

            return View();
        }

        public ActionResult CrearCuenta()
        {
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
        [HttpPost]
        public ActionResult CreateAddress(AntBoxAddressViewModel model)
        {
            var id = ((AntBoxAddressViewModel)(Session["customer"])).Customer_id;

            if (Session["customer"] == null)
                return PartialView(model);

            if (ModelState.IsValid)
            {
                AddressRequestOptions request = Mapper.Map<AddressRequestOptions>(model);

                request.Customer_id = id;

                return CreateAddress(request);

            }


            return View(model);

        }


        public ActionResult GuardaTempTasks(string form)
        {
            //guardamos de forma temporal las direcciones y tareas en la session
            Session["TasksTemp"] = form;

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

        


        private JsonResult CreateAddress(AddressRequestOptions address)
        {
            var addresService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addresService.CreateAddress(address);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private JsonResult UpdateAddress(string idAddress, AddressUpdateOptions address)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addressService.UpdateAddress(address, idAddress);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [ErrorHandler]
        private JsonResult ListAddresses(string idcustomer)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());   

            var result = addressService.ListAddresses(idcustomer);

            var antBoxResult = new List<AntBoxAddressViewModel>();

            result.ForEach(r =>
            {
                var dir = addressService.SearchAddress(r.Id);

                var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);

                antBoxResult.Add(map);
            });

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private JsonResult GetAddress(string id)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addressService.SearchAddress(id);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private JsonResult DeleteAdress(string idAddress)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addressService.DeleteAddress(idAddress);

            return Json(result, JsonRequestBehavior.AllowGet);
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

            return Json(new { result = delegations }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetColonias(string zipcode = null)
        {
            var col = new List<SelectListItem>();
          

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


            return Json(new { result = col }, JsonRequestBehavior.AllowGet);

        }


        #endregion





    }
}