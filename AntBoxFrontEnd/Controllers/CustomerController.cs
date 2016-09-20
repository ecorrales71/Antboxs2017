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
using AntBoxFrontEnd.Services.Tasks;

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

                //boxes
                var servCajas = new BoxesService(ServiceConfiguration.GetApiKey());

                var cajas = new List<BoxesResponse>();

                cajas= servCajas.ListBoxes(StatusBoxes.Active);

                var cajasDTO = new List<AntBox>();
                var lineOrders = new List<LineOrder>();

                if(cajas != null)
                {
                    cajas.ForEach(x =>
                    {
                        cajasDTO.Add(new AntBox
                        {
                            Sizes = x.Size,
                            Secure = x.Secure,
                            Description = x.Label,
                            Label = x.Label,
                            Model = x.Model,
                            Price = x.Price
                        });


                       // cajasDTO.Add(Mapper.Map<BoxesResponse, AntBox>(x));
                    });

                    cajasDTO.ForEach(x =>
                    {
                        lineOrders.Add(
                            new LineOrder {
                                Description = x.Description,
                                Label = x.Label,
                                LineTotal = 0,
                                Model = x.Model,
                                Price = x.Price,
                                Quantity = 0,
                                Secure = x.Secure,
                                Sizes = x.Sizes
                            });
                        
                        
                        
                        //lineOrders.Add(Mapper.Map<AntBox, LineOrder>(x));
                    });
                }
               


                //order
                var orderModel = new OrderViewModel();
                var BoxesModel = new AntBoxesViewModel();
                
                BoxesModel.Order = lineOrders;
                BoxesModel.ActiveAntBoxes = cajasDTO;
                

                BoxesModel.Discount = 0;
                BoxesModel.Iva = Convert.ToDecimal( WebConfigurationManager.AppSettings["Iva"]);
                BoxesModel.OrderTotal = 0;
                BoxesModel.Subtotal = 0;
                BoxesModel.Total = 0;


                orderModel.Boxes = BoxesModel;

                //addresss
                var addressService = new AddressService(ServiceConfiguration.GetApiKey());

                var result = addressService.ListAddresses(customer.Id);

                var antBoxResult = new List<AntBoxAddressViewModel>();

                if (result != null && result.Count > 0)
                {
                    result.ForEach(r =>
                    {
                        var dir = addressService.SearchAddress(r.Id);

                        var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);

                        antBoxResult.Add(map);
                    });

                    orderModel.Addresses = antBoxResult;
                }
                else
                {
                    orderModel.Addresses = new List<AntBoxAddressViewModel>();

                }

                //CARDS

                var ps = new PaymentService(ServiceConfiguration.GetApiKey());

                List<CardObject> cards =  ps.ListPaymetCards(customer.Id);
                
                orderModel.Cards = cards==null?new List<CardObject>():cards;

                return orderModel;


            }
            catch (Exception ex)
            {

                return null;

            }           
        }

        public JsonResult getSchedules(string date)
        {

            try
            {

                var srvice = new TaskService(ServiceConfiguration.GetApiKey());

                var schedules = srvice.ListSchedules(date);

                return Json(schedules, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL LISTAR LOS HORARIOS DISPONIBLES" }, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]
        public JsonResult GetCardsAjax()
        {
            var ps = new PaymentService(ServiceConfiguration.GetApiKey());

            List<CardObject> result = new List<CardObject>();
            try
            {
                result = ps.ListPaymetCards(((CustomerResponse)Session["customer"]).Id);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL LISTAR LAS TARJETAS DISPONIBLES" }, JsonRequestBehavior.AllowGet);

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
