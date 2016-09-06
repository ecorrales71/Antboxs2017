using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Models;
using AutoMapper;
using System.Dynamic;
using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Boxes;

namespace AntBoxFrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(Session["customer"]);
        }

        public ActionResult Cuenta()
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(Session["customer"]);
        }

        public ActionResult Direcciones()
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //CustomerResponse customer = (CustomerResponse)Session["customer"];

            //var addressService = new AddressService(ServiceConfiguration.GetApiKey());

            //if (customer == null)
            //    return View();

            ////var res = l.ListAddresses(customer.Id);

            //var result = addressService.ListAddresses(customer.Id);

            //var antBoxResult = new List<AntBoxAddressViewModel>();

            //result.ForEach(r =>
            //{
            //    var dir = addressService.SearchAddress(r.Id);

            //    var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);

            //    antBoxResult.Add(map);
            //});




            //return View(antBoxResult);
            return View();
        }

        public ActionResult Antboxs()
        {
            return View();
        }
        public ActionResult Movimientos()
        {
            return View();
        }
        //public ActionResult Ordenar()
        //{
        //    return View();
        //}
        public ActionResult Pagos()
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            List<CardObject> result = new List<CardObject>();
            try
            {
                result = ps.ListPaymetCards(((CustomerResponse)Session["customer"]).Id);
            }
            catch (Exception ex)
            {
            }

            return View(result);
        }


        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }



        #region Boxes

        public ActionResult Ordenar()
        {
            var addresses = new List<AddressResponse>();

            

            try
            {
                if (Session["customer"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                CustomerResponse customer = (CustomerResponse)Session["customer"];



                var addressServ = new AddressService(ServiceConfiguration.GetApiKey());

                addresses = addressServ.ListAddresses(customer.Id);

                var availableAddresses = new List<AntBoxAddressViewModel>();
                
                addresses.ForEach(a=> {
                    availableAddresses.Add(new AntBoxAddressViewModel
                    {
                        Id = a.Id,
                        Alias = a.Alias

                    });
                });





                var boxes = ListActiveAntBoxes();

                var antboxes = new AntBoxesViewModel
                {
                    Discount = 0,
                    Iva = 0,
                    OrderTotal = 0,
                    Subtotal = 0,
                    Total = 0,
                    ActiveAntBoxes = boxes
                };



                var cardServices = new PaymentService(ServiceConfiguration.GetApiKey());

                var listCards = cardServices.ListPaymetCards(customer.Id);

                ViewData["Boxes"] = boxes;
                ViewData["Antboxes"] = antboxes;
                ViewData["AvailableAddresses"] = availableAddresses;
                ViewData["ListCards"] = listCards;

                ViewData["AntBoxOrder"] = new AntBoxesViewModel()
                {
                    Iva = 0,
                    Discount = 0,
                    Subtotal = 0,
                    Total = 0,
                    OrderTotal = 0            
                };


            }
            catch(Exception ex)
            {
                return View();
            }           

             return View();
        }

        public JsonResult getActiveAntBoxes()
        {
            var boxes = new List<AntBox>();

            try
            {
                var service = new BoxesService(ServiceConfiguration.GetApiKey());

                var results = service.ListBoxes(); //solo las activas

                results.ForEach(b =>
                {

                    boxes.Add(Mapper.Map<BoxesResponse, AntBox>(b));

                });

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
            }

            return Json(new { result = boxes }, JsonRequestBehavior.AllowGet);

        }

        private List<AntBox> ListActiveAntBoxes()
        {
            var boxes = new List<AntBox>();

            try
            {
                var service = new BoxesService(ServiceConfiguration.GetApiKey());

                var results = service.ListBoxes(); //solo las activas

                results.ForEach(b =>
                {

                    boxes.Add(Mapper.Map<BoxesResponse, AntBox>(b));

                });

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message + " " + ex.InnerException, LogManager.Error);
            }

            return boxes;

        }
        #endregion


    }
}
