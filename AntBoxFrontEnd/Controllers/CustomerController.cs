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
using System.Web.Configuration;

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

            var order = GetNewOrder();










            return View(order);
        }


        private OrderViewModel GetNewOrder()
        {
            try
            {
                if (Session["customer"] == null)
                {
                    return null;
                }

                CustomerResponse customer = (CustomerResponse)Session["customer"];

                var servCajas = new BoxesService(ServiceConfiguration.GetApiKey());

                var cajas = servCajas.ListBoxes(StatusBoxes.Active);
                var cajasDTO = new List<AntBox>();

                cajas.ForEach(x =>
                {
                    cajasDTO.Add(Mapper.Map<BoxesResponse, AntBox>(x));
                });

                var lineOrders = new List<LineOrder>();

                cajasDTO.ForEach(x =>
                {
                    lineOrders.Add(Mapper.Map<AntBox, LineOrder>(x));
                });


                var orderModel = new OrderViewModel();

                var BoxesModel = new AntBoxesViewModel();



                BoxesModel.Order = lineOrders;

                BoxesModel.ActiveAntBoxes = cajasDTO;




                BoxesModel.Discount = 0;
                BoxesModel.Iva = Convert.ToDecimal( WebConfigurationManager.AppSettings["Iva"]);
                BoxesModel.Order = new List<LineOrder>() { new LineOrder {LineTotal=0,Quantity = 1 } };
                BoxesModel.OrderTotal = 0;
                BoxesModel.Subtotal = 0;
                BoxesModel.Total = 0;


                orderModel.Boxes = BoxesModel;

                var addressService = new AddressService(ServiceConfiguration.GetApiKey());

                var result = addressService.ListAddresses(customer.Id);

                var antBoxResult = new List<AntBoxAddressViewModel>();

                if (result.Count > 0)
                {
                    result.ForEach(r =>
                    {
                        var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(r);

                        antBoxResult.Add(map);
                    });

                    orderModel.Addresses = antBoxResult;
                }
               

                return orderModel;


            }
            catch (Exception ex)
            {

                return null;

            }           
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
