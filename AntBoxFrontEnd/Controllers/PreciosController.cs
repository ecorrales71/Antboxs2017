using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Services.Coupon;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.Rules;
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
            Session["couponidhome"] = null;
            Session["AntBoxesOrder"] = null;
            Session["TasksTemp"] = null;
            Session["CouponID"] = null;

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

                var apikey = ServiceConfiguration.GetApiKey();

                var servCajas = new BoxesService(apikey);

                var servRules = new RulesService(apikey);

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
                orderModel.Rules = servRules.ListRules();

                //addresss
                var addressService = new AddressService(apikey);

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

                var ps = new PaymentService(apikey);

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

        private string CheckOutBox(string workerid, string address)
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

            string coupon = null;
            if (!String.IsNullOrEmpty(Session["couponidhome"].ToString()))
            {
                coupon = Session["couponidhome"].ToString();
            }


            var order = new AntBoxRequestOptions()
            {
                Checkouts = checkouts,

                Customer_id = ((CustomerResponse)Session["customer"]).Id,

                Worker_id = workerid,

                Coupon_id = coupon,

                Address_id = address
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


        private bool CreatePickupTask(string from, string to, string timefrom, string idAddress, string worker, string folio, bool esperar = false)
        {
            /*var fromDate = GetDateFromString(from, timefrom);

            var toDate = fromDate.AddHours(RANGE_HOURS_TO_TASK);*/

            var ts = new TaskService(ServiceConfiguration.GetApiKey());
            var t = new TaskRequestOption
            {
                Address_id = idAddress,
                customer_id = ((CustomerResponse)Session["customer"]).Id,
                from = Convert.ToInt64(from),
                To = Convert.ToInt64(to),
                Worker_id = worker,
                Folio = folio,
                Type = ts.Type_Pickup,
                Service_time = esperar
            };
            return ts.CreateTask(t);
        }
        private bool CreateDeliveryTask(string from, string to, string timefrom, string idAddress, string worker, string folio, string device)
        {
            /*var fromDate = GetDateFromString(from, timefrom);

            var toDate = fromDate.AddHours(RANGE_HOURS_TO_TASK);*/

            var ts = new TaskService(ServiceConfiguration.GetApiKey());
            var t = new TaskRequestOption
            {
                Address_id = idAddress,
                customer_id = ((CustomerResponse)Session["customer"]).Id,
                from = Convert.ToInt64(from),
                To = Convert.ToInt64(to),
                Worker_id = worker,
                Folio = folio,
                Type = ts.Type_Delivery,
                Device_id = device
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
                    Lastname2 = requestOrder.Lastname2,
                    Mobilephone = requestOrder.Mobilephone,
                    Password = requestOrder.Password,
                    Username = requestOrder.Username,
                    Status = true,
                    Phone = requestOrder.Phone
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
                            return Json(new { success = false, verif = 2, responseText = "Ocurrio un error al agregar dirección" }, JsonRequestBehavior.AllowGet);
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
                                return Json(new { success = false, verif = 3, responseText = "Ocurrio un error al registrar la tarjeta" }, JsonRequestBehavior.AllowGet);
                            else
                            {
                                //PASO 3 - REALIZAR PEDIDO DE CAJAS VACIAS
                                bool waitTimeWorker;
                                if (string.IsNullOrEmpty(modeltask.Esperar))
                                    waitTimeWorker = false;
                                else
                                    waitTimeWorker = Convert.ToBoolean(modeltask.Esperar.ToLower());

                                var folioRecoleccion = CheckOutBox(modeltask.Horario, dirRec);
                                bool isTaskPickupCreated;
                                if (string.IsNullOrEmpty(folioRecoleccion))
                                    return Json(new { success = false, verif = 4, responseText = "Ocurrio un error al solicitar las cajas de recoleccion" }, JsonRequestBehavior.AllowGet);
                                else
                                {
                                    //PASO 4 - AGENDAR TASK
                                    isTaskPickupCreated = CreateDeliveryTask(modeltask.From, modeltask.To, modeltask.HoraRecoleccionString, dirRec, modeltask.Horario, folioRecoleccion, requestOrder.Deviceid);
                                    if (!isTaskPickupCreated)
                                        return Json(new { success = false, verif=5, responseText = "Ocurrio un error al crear la tarea de recoleccion" }, JsonRequestBehavior.AllowGet);
                                    result += "Tarea de recoleccion agendada: " + isTaskPickupCreated + "\n";

                                    Session["TaskTemp"] = null;

                                    return Json(result, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, verif = 4, responseText = "Ocurrio un error al solicitar las cajas de recolección" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, verif = 1, responseText = "Ocurrio un error al crear el usuario" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, verif=7, responseText = "Ocurrio al procesar la orden" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDisccount(string cupon, string version)
        {
            string couponname = "couponidhome";
            if (version == "1")
            {
                couponname = "couponidadmin";
            }


            var servicio = new CouponService(ServiceConfiguration.GetApiKey());
            List<CouponResponse> result = new List<CouponResponse>();
            result = servicio.SearchCouponName(cupon);

            if (result != null)
            {
                DateTime dateini = formatdate(result[0].From, "yyyy-MM-dd");
                DateTime datefin = formatdate(result[0].To, "yyyy-MM-dd");
                DateTime today = DateTime.Today;

                if (today.Ticks >= dateini.Ticks && today.Ticks <= datefin.Ticks)
                {
                    Session[couponname] = result[0].Id;
                    return Json(new { success = true, resposeText = "aplica", discount = result[0].Discount, couponid = result[0].Id, verif = 1 }, JsonRequestBehavior.AllowGet);
                    
                }
                else
                {
                    Session[couponname] = null;
                    return Json(new { success = false, resposeText = "no aplica" }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                string customeri = null;
                if (version == "1")
                {
                    customeri = ((CustomerResponse)Session["customer"]).Id;
                }
                var verif = servicio.Referer(customeri, cupon);
                if (!verif)
                {
                    Session[couponname] = null;
                    return Json(new { success = false, resposeText = "no existe" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Session[couponname] = cupon;
                    return Json(new { success = true, resposeText = "aplica2", verif = 2 }, JsonRequestBehavior.AllowGet);
                }
            }

            //convertir fecha from to to date time
            //verificar si aplica y devolver true o false

            return Json(new { success = result }, JsonRequestBehavior.AllowGet);
        }

        private DateTime formatdate(string date, string format)
        {
            DateTime dt = DateTime.ParseExact(date, format,
                                System.Globalization.CultureInfo.InvariantCulture);
            return dt;
        }

    }
}