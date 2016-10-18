using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.Tasks;
using AutoMapper;
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

        private const int  RANGE_HOURS_TO_TASK = 2;
        

        public ActionResult Index()
        {
            
             var   model = GetNewOrder();


            return View("Precios",model);
        }

     


        private OrderViewModel GetNewOrder()
        {
            try
            {
                //if (Session["customer"] == null)
                //{
                //    return null;
                //}

                //CustomerResponse customer = (CustomerResponse)Session["customer"];

                //boxes
                var servCajas = new BoxesService(ServiceConfiguration.GetApiKey());

                var cajas = new List<BoxesResponse>();

                cajas = servCajas.ListBoxes(StatusBoxes.Active);

                var cajasDTO = new List<AntBox>();
                var lineOrders = new List<LineOrder>();

                if (cajas != null)
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
                            new LineOrder
                            {
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
                BoxesModel.Iva = Convert.ToDecimal(WebConfigurationManager.AppSettings["Iva"]);
                BoxesModel.OrderTotal = 0;
                BoxesModel.Subtotal = 0;
                BoxesModel.Total = 0;


                orderModel.Boxes = BoxesModel;

                //addresss
                var addressService = new AddressService(ServiceConfiguration.GetApiKey());

                //var result = addressService.ListAddresses(customer.Id);

                //var antBoxResult = new List<AntBoxAddressViewModel>();

                //if (result != null && result.Count > 0)
                //{
                //    result.ForEach(r =>
                //    {
                //        var dir = addressService.SearchAddress(r.Id);

                //        var map = Mapper.Map<AddressResponse, AntBoxAddressViewModel>(dir);

                //        antBoxResult.Add(map);
                //    });

                //    orderModel.Addresses = antBoxResult;
                //}
                //else
                //{
                //    orderModel.Addresses = new List<AntBoxAddressViewModel>();

                //}

                //CARDS

                var ps = new PaymentService(ServiceConfiguration.GetApiKey());

                //List<CardObject> cards = ps.ListPaymetCards(customer.Id);

                //orderModel.Cards = cards == null ? new List<CardObject>() : cards;

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
            catch (Exception ex)
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




        public JsonResult DoOrder(string dirRecolId, string dirEntlId,
                                string fecRec, string fecEnt, string workerEnt,
                                string workerRec, string horaEnt, string horaRec,
                                string esperar, string contactoTel, string contactoMail,
                                string referenciasRec, string referenciasEnt, string cardid, string monto)
        {
            string result = "";

            try
            {
                bool waitTimeWorker;

                if (string.IsNullOrEmpty(esperar))
                    waitTimeWorker = false;
                else
                    waitTimeWorker = Convert.ToBoolean(esperar.ToLower());

                var folioRecoleccion = CheckOutBox(workerRec);

                bool isTaskPickupCreated;
                if (string.IsNullOrEmpty(folioRecoleccion))
                    return Json(new { success = false, responseText = "OCURRIO UN ERROR AL SOLICITAR LAS CAJAS DE RECOLECCION" }, JsonRequestBehavior.AllowGet);

                isTaskPickupCreated = CreatePickupTask(fecRec, horaRec, dirRecolId, workerRec, folioRecoleccion, waitTimeWorker);
                if (!isTaskPickupCreated)
                    return Json(new { success = false, responseText = "OCURRIO UN ERROR AL CREAR TAREA DE RECOLECCION" }, JsonRequestBehavior.AllowGet);

                result += "Tarea de recoleccion agendada: " + isTaskPickupCreated + "\n";

                // var status = DoCharge(Convert.ToDecimal(monto));

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL LISTAR LAS TARJETAS DISPONIBLES" }, JsonRequestBehavior.AllowGet);
            }
        }


        private string CheckOutBox(string workerid)
        {
            var boxes = GetAntboxesTemp();

            var checkouts = new CheckOut[boxes.Count];

            int i = 0;

            foreach (var item in boxes)
            {
                checkouts[i] = new CheckOut()
                {
                    Box_id = item.Key,
                    Quantity = item.Value
                };
                i++;
            }

            var order = new AntBoxRequestOptions()
            {
                Checkouts = checkouts,

                Customer_id = ((CustomerResponse)Session["customer"]).Id,

                Worker_id = workerid
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
                Antboxs = serials,

                Customer_id = ((CustomerResponse)Session["customer"]).Id,

                Worker_id = workerid
            };

            var serv = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            var res = serv.CreateAntBoxes(order);

            return res;
        }


        private bool CreatePickupTask(string from, string timefrom, string idAddress, string worker, string folio, bool esperar = false)
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
            int.TryParse(time.Substring(0, 2), out hour);

            var dateForm = Convert.ToDateTime(date);

            var dateResult = DateHelpers.AppendTimeToDate(dateForm, new TimeSpan(hour, 0, 0));

            return dateResult;
        }


        private ChargeResponse DoCharge(decimal total, string folio)
        {
            var service = new PaymentService(ServiceConfiguration.GetApiKey());

            var response = service.DoChargeToCustomer(new Charge() { Customer_id = ((CustomerResponse)Session["customer"]).Id, Amount = total, Folio = folio });

            return response;
        }


        [HttpPost]
        public ActionResult MakePaymentAjax(string deviceId, string state, string token)
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
                Charge c = new Charge()
                {
                    Amount = 200,
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
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ProcesarOrdenRegistro(ProcessOrderModel requestOrder)
        {
            try
            {

                var cus = new AntBoxFrontEnd.Services.Customer.CustomerRequestOptions
                {
                    Email = requestOrder.Email,
                    Name = requestOrder.Name,
                    LastName = requestOrder.Lastname,
                    //Lastname2 = "Prueba Apellido materno4",
                    Mobile_phone = "",
                    Password = requestOrder.Password,
                    Username = requestOrder.Username,
                    Status = true
                };

                var ser = new CustomerServices(ServiceConfiguration.GetApiKey());

                var res = ser.CreateCustomer(cus);
                if (res)
                {
                    var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
                    {
                        Email = requestOrder.Email,
                        Password = requestOrder.Password
                    };

                    LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

                    string id = ls.HovaLogin(usr);

                    CustomerServices cs = new CustomerServices(ServiceConfiguration.GetApiKey());

                    CustomerResponse customer = cs.SearchCustomer(id);

                    Session["customer"] = customer;

                    string result = "";
                
                    if (Session["TasksTemp"] != null)
                    {
                        //PASO1 - AGREGAR DIRECCION
                        AgendTaskModel modeltask = (AgendTaskModel)Session["TasksTemp"];

                        AddressRequestOptions requestOption = new AddressRequestOptions
                        {
                            Alias = "Direccion 1",
                            Zipcode = modeltask.Zipcode,
                            Street = modeltask.Street,
                            External_number = modeltask.External_number,
                            Internal_number = modeltask.Internal_number,
                            Neighborhood = modeltask.Neighborhood,
                            Delegation = modeltask.Delegation,
                            City = modeltask.City,
                            Country = modeltask.Country,
                            Customer_id = customer.Id,
                            State = modeltask.State
                        };

                        var addresService = new AddressService(ServiceConfiguration.GetApiKey());
                        var resultAddress = addresService.CreateAddressForCustomer(requestOption);
                        if (resultAddress == null)
                            return Json(new { success = false, responseText = "OCURRIO UN ERROR AL AGREGAR LA DIRECCION" }, JsonRequestBehavior.AllowGet);
                        else
                        {
                            var dirRec = resultAddress.Id;

                            //PASO 2 - AGREGAR TARJETA
                            var ps = new PaymentService(ServiceConfiguration.GetApiKey());
                            PaymentRequestOptions pro = new PaymentRequestOptions
                            {
                                Customer_id = ((CustomerResponse)Session["customer"]).Id,
                                Device_id = requestOrder.Deviceid,
                                State = requestOrder.State,
                                Token = requestOrder.Token
                            };
                            bool resultCard = ps.CreatePaymentCard(pro);
                            if (!resultCard)
                                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL PAGO" }, JsonRequestBehavior.AllowGet);
                            else
                            {
                                //PASO 3 - REALIZAR PEDIDO DE CAJAS VACIAS
                                bool waitTimeWorker;
                                if (string.IsNullOrEmpty(modeltask.Esperar))
                                    waitTimeWorker = false;
                                else
                                    waitTimeWorker = Convert.ToBoolean(modeltask.Esperar.ToLower());

                                var folioRecoleccion = CheckOutBox(modeltask.Horario);
                                bool isTaskPickupCreated;
                                if (string.IsNullOrEmpty(folioRecoleccion))
                                    return Json(new { success = false, responseText = "OCURRIO UN ERROR AL SOLICITAR LAS CAJAS DE RECOLECCION" }, JsonRequestBehavior.AllowGet);
                                else
                                {
                                    //PASO 4 - AGENDAR TASK
                                    isTaskPickupCreated = CreatePickupTask(modeltask.Fecha_recoleccion, modeltask.HoraRecoleccionString, dirRec, modeltask.Horario, folioRecoleccion, waitTimeWorker);
                                    if (!isTaskPickupCreated)
                                        return Json(new { success = false, responseText = "OCURRIO UN ERROR AL CREAR TAREA DE RECOLECCION" }, JsonRequestBehavior.AllowGet);
                                    result += "Tarea de recoleccion agendada: " + isTaskPickupCreated + "\n";

                                    Session["TaskTemp"] = null;

                                    return Json(result, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, responseText = "OCURRIO UN ERROR AL SOLICITAR LAS CAJAS DE RECOLECCION" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL PROCESAR LA ORDEN" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public decimal GetDisccount(string codgo)
        {
            return Convert.ToDecimal(0.00);
        }

    }
}