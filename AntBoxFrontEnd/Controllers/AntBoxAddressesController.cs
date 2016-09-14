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
    public class AntBoxAddressesController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            var vm = new AntBoxAddressViewModel();           

            return PartialView(vm);

        }
        [HttpPost]
        public ActionResult Create(AntBoxAddressViewModel model)
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

        private JsonResult ListAddresses(string idcustomer)
        {
            var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            var result = addressService.ListAddresses(idcustomer);

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

        [HttpPost]
        public ActionResult ValidateZipCodeAjax(string zipcode)
        {
            var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = service.SearchZipCode(zipcode);
            if (result.Count <1)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> GetStates(string zipcode = null)
        {
            var states = new List<SelectListItem>();
            if (zipcode == null)
                return states;

            var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = service.SearchZipCode(zipcode);

            result.ForEach(x =>
            {
                states.Add(new SelectListItem
                {
                    Text = x.State,
                    Value = x.State
                });
            });

            return states;

        }

        public IEnumerable<SelectListItem> GetColonias(string zipcode = null)
        {
            var col = new List<SelectListItem>();
            if (zipcode == null)
                return col;

            var service = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = service.SearchZipCode(zipcode);           

            result.ForEach(x =>
            {
                col.Add(new SelectListItem
                {
                    Text = x.Neighborhood,
                    Value = x.Neighborhood
                });
            });

            return col;

        }

    }
}