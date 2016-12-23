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
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.BillingAddress;
using AntBoxFrontEnd.Services.Rules;
using Newtonsoft.Json;

namespace AntBoxFrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        private const int RANGE_HOURS_TO_TASK = 2;



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
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var servicio = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            PaginationAntBoxes result = new PaginationAntBoxes();
            try
            {
                result = servicio.ListAntBoxes(((CustomerResponse)Session["customer"]).Id, AntBoxStatusEnum.Defualt, 1);
            }
            catch (Exception ex)
            {
            }
            if (result == null)
            {
                result = new PaginationAntBoxes();
            }

            var addressService = new AddressService(ServiceConfiguration.GetApiKey());
            var resultAddress = addressService.ListAddresses(((CustomerResponse)Session["customer"]).Id);
            var antBoxResult = new List<AntBoxAddressViewModel>();

            if (resultAddress != null && resultAddress.Count > 0)
            {
                resultAddress.ForEach(r =>
                {
                    var dir = addressService.SearchAddress(r.Id);
                    var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);
                    antBoxResult.Add(map);
                });

                result.Addresses = antBoxResult;
            }
            else
            {
                result.Addresses = new List<AntBoxAddressViewModel>();
            }

            return View(result);
        }
        public ActionResult Movimientos()
        {
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var apikey = ServiceConfiguration.GetApiKey();
            var servicio = new TaskService(apikey);
            var addressService = new AddressService(apikey);
            var resultAddress = addressService.ListAddresses(((CustomerResponse)Session["customer"]).Id);
            var addresses = new List<AntBoxAddressViewModel>();

            PaginationCustomerTask result = new PaginationCustomerTask();
            try
            {
                result = servicio.ListTaskByCustomer(((CustomerResponse)Session["customer"]).Id, 1);
                if (resultAddress != null && resultAddress.Count > 0)
                {
                    resultAddress.ForEach(r =>
                    {
                        var dir = addressService.SearchAddress(r.Id);
                        var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);
                        addresses.Add(map);
                    });

                    result.Addresses = addresses;
                }
                else
                {
                    result.Addresses = new List<AntBoxAddressViewModel>();
                }
                result.Cliente = ((CustomerResponse)Session["customer"]);
            }
            catch (Exception ex)
            {
            }

            return View(result);
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

            var apikey = ServiceConfiguration.GetApiKey();
            var ps = new PaymentService(apikey);
            var service = new PaymentService(apikey);
            PagosModel model = new PagosModel();

            List<CardObject> result = new List<CardObject>();
            List<BillingAddressResponse> resultaddress = new List<BillingAddressResponse>();
            List<ChargeResponse> resultPayments = new List<ChargeResponse>();
            try
            {
                result = ps.ListPaymetCards(((CustomerResponse)Session["customer"]).Id);
                resultPayments = service.ListCharges(((CustomerResponse)Session["customer"]).Id);

                var servicio = new BillingAddressService(apikey);
                resultaddress = servicio.ListBillingAddresses(((CustomerResponse)Session["customer"]).Id, 1);

            }
            catch (Exception ex)
            {
            }

            model.Cards = result;
            model.Address = resultaddress;
            model.Payments = resultPayments;

            return View(model);
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
            if (Session["customer"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var order = GetNewOrder();
            Session["couponidadmin"] = null;
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
                var apikey = ServiceConfiguration.GetApiKey();
                //boxes
                var servCajas = new BoxesService(apikey);
                var serviceRules = new RulesService(apikey);
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
                            Price = x.Price,
                            Id = x.Id
                            
                        });


                       // cajasDTO.Add(Mapper.Map<BoxesResponse, AntBox>(x));
                    });

                    cajasDTO.ForEach(x =>
                    {
                        lineOrders.Add(
                            new LineOrder {
                                Id = x.Id,
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
                orderModel.Rules = serviceRules.ListRules();
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


        public JsonResult TempUpdateAntboxes(string boxid, int quantity)
        {
            try
            {
                var boxes = new Dictionary<string, int>();


                if (Session["AntBoxesOrder"] == null)
                    Session["AntBoxesOrder"] = boxes;


                boxes = Session["AntBoxesOrder"] as Dictionary<string, int>;

                boxes[boxid] = quantity;
                
                Session["AntBoxesOrder"] = boxes;

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL LISTAR LAS TARJETAS DISPONIBLES" }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult TempUpdateAntboxesObject(List<boxsResponse> boxs, string couponid)
        {
            try
            {
                var boxes = new Dictionary<string, int>();

                Session["AntBoxesOrder"] = boxes;

                boxes = Session["AntBoxesOrder"] as Dictionary<string, int>;

                foreach(var box in boxs)
                {
                    boxes[box.boxid] = box.quantity;
                }

                Session["AntBoxesOrder"] = boxes;
                Session["couponidhome"] = couponid;

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL LISTAR LAS TARJETAS DISPONIBLES" }, JsonRequestBehavior.AllowGet);

            }
        }


        private Dictionary<string, int> GetAntboxesTemp()
        {
            try
            {
                var boxes = new Dictionary<string, int>();


                if (Session["AntBoxesOrder"] != null)
                    boxes = Session["AntBoxesOrder"] as Dictionary<string, int>;


                return boxes;
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return new Dictionary<string, int>();
            }
        }




        public JsonResult DoOrder(string dirRecolId, string dirEntId, string fecRec, string fecEnt, string workerRec, string workerEnt, string couponid,
                                string horaRec, string horaEnt, string esperar, string contactoTel , string contactoMail,
                                string referenciasRec, string referenciasEnt, string cardid, string monto, List<boxsResponse> boxs)
        {
            string result = "";

            try
            {
                bool waitTimeWorker;

                if (string.IsNullOrEmpty(esperar))
                    waitTimeWorker = false;
                else
                    waitTimeWorker = Convert.ToBoolean(esperar.ToLower());

                var folioRecoleccion = CheckOutBox(workerRec, boxs);

                bool isTaskPickupCreated;
                if (string.IsNullOrEmpty(folioRecoleccion))
                    return Json(new { success = false, responseText = "OCURRIO UN ERROR AL SOLICITAR LAS CAJAS DE RECOLECCION" }, JsonRequestBehavior.AllowGet);

                isTaskPickupCreated = CreateDeliveryTask(fecRec, horaRec, dirRecolId, workerRec, folioRecoleccion);

                if (!isTaskPickupCreated)
                    return Json(new { success = false, responseText = "OCURRIO UN ERROR AL CREAR TAREA DE RECOLECCION" }, JsonRequestBehavior.AllowGet);

                if (!waitTimeWorker)
                {
                    bool isTaskDeliveryCreated;
                    isTaskDeliveryCreated = CreatePickupTask(fecEnt, horaEnt, dirEntId, workerEnt, folioRecoleccion);

                    if (!isTaskDeliveryCreated)
                        return Json(new { success = false, responseText = "OCURRIO UN ERROR AL CREAR TAREA DE RECOLECCION" }, JsonRequestBehavior.AllowGet);
                }

                

                result += "Tarea de recoleccion agendada: " + isTaskPickupCreated + "\n";

                decimal VALOR_TEST = 10;

                // var status = DoCharge(Convert.ToDecimal(monto));

                /*var status = DoCharge(Convert.ToDecimal(VALOR_TEST), folioRecoleccion);
                if (status == null || string.IsNullOrEmpty(status.Status))
                    return Json(new { success = false, responseText = "OCURRIO UN ERROR Al REALIZAR EL CARGO" }, JsonRequestBehavior.AllowGet);*/
                //result += "Cargo realizado: " + status.Status;// +" el día " + status.Creation_date;

                return Json(new { success = true, responseText = "Tu pedido ha sido registrado con éxito y llegará en la fecha solicitada. A continuación te llegará un correo electrónico confirmando tu(s) Antboxs. En caso de que no te llegue el correo, no te preocupes el mismo ya fue registrado en nuestro sistema." }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL PROCESAR LA ORDEN" }, JsonRequestBehavior.AllowGet);
            }
        }


        private string CheckOutBox(string workerid, List<boxsResponse> boxs)
        {
            var boxes = boxs;

            var checkouts = new CheckOut[boxes.Count];

            int i = 0;

            foreach(var item in boxes)
            {
                checkouts[i] = new CheckOut()
                {
                    Box_id = item.boxid,
                    Quantity = item.quantity
                };
                i++;
            }

            string coupon = null;
            if (Session["couponidadmin"] != null)
            {
                coupon = Session["couponidadmin"].ToString();
            }

            var order = new AntBoxRequestOptions()
            {
                Checkouts = checkouts,

                Customer_id = ((CustomerResponse)Session["customer"]).Id,

                Worker_id = workerid,

                Coupon_id = coupon
            };

            var serv = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            var res = serv.CreateAntBoxes(order);

            return res;



        }


        private string Delivery(string boxid, string[] serials, string workerid)
        {
            var boxes = GetAntboxesTemp();

            var checkouts = new CheckOut[boxes.Count];

            int i = 0;

            var ids = new List<AntBoxObjectCheckout>();

            foreach (var item in serials)
            {
                ids.Add(new AntBoxObjectCheckout
                {
                    Id = boxid,
                    Serial = item
                });
            }

            foreach (var item in boxes)
            {
                checkouts[i] = new CheckOut()
                {
                    Box_id = item.Key,
                    Quantity = item.Value
                };
            }

            var order = new AntBoxRequestOptions()
            {
                Antboxs = ids,

                Customer_id = ((CustomerResponse)Session["customer"]).Id,

                Worker_id = workerid
            };

            var serv = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            var res = serv.CreateAntBoxes(order);

            return res;
        }


        private bool CreatePickupTask(string from, string timefrom,  string idAddress, string worker, string folio , bool esperar = false)
        {
            var fromDate = GetDateFromString(from, timefrom);

            var toDate = fromDate.AddHours(RANGE_HOURS_TO_TASK);

            var ts = new TaskService(ServiceConfiguration.GetApiKey());
            var t = new TaskRequestOption
            {
                Address_id = idAddress,
                customer_id = ((CustomerResponse)Session["customer"]).Id,
                from = DateHelpers.ToUnixTime(fromDate),
                To = DateHelpers.ToUnixTime(toDate),
                Worker_id = worker,
                Folio = folio,
                Type = ts.Type_Pickup,
                Service_time = esperar
            };
            return ts.CreateTask(t);
        }
        private bool CreateDeliveryTask(string from, string timefrom, string idAddress, string worker, string folio)
        {
            var fromDate = GetDateFromString(from, timefrom);

            var toDate = fromDate.AddHours(RANGE_HOURS_TO_TASK);

            var ts = new TaskService(ServiceConfiguration.GetApiKey());
            var t = new TaskRequestOption
            {
                Address_id = idAddress,
                customer_id = ((CustomerResponse)Session["customer"]).Id,
                from = DateHelpers.ToUnixTime(fromDate),
                To = DateHelpers.ToUnixTime(toDate),
                Worker_id = worker,
                Folio = folio,
                Type = ts.Type_Delivery
            };
            return ts.CreateTask(t);
        }

        private DateTime GetDateFromString(string date, string time)
        {
            int hour = 0;
            int.TryParse( time.Substring(0, 2), out hour);

            var dateForm = Convert.ToDateTime(date);

            var dateResult = DateHelpers.AppendTimeToDate(dateForm, new TimeSpan(hour, 0, 0));

            return dateResult;
        }


        private ChargeResponse DoCharge(decimal total, string folio)
        {
            var service = new PaymentService(ServiceConfiguration.GetApiKey());

            var response = service.DoChargeToCustomer(new Charge() {
                Customer_id = ((CustomerResponse)Session["customer"]).Id, 
                Amount= total,
                Folio = folio,
                Type = "payment"
            });

            return response;
        }

        public ActionResult Logout()
        {
            Session["customer"] = null;

            return RedirectToAction("Index", "Home");
        }

        public class boxsResponse
        {
            public string boxid { get; set; }
            public int quantity { get; set; }
        }


        #endregion


    }
}
