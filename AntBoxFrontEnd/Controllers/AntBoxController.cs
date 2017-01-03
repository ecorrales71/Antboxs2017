using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Models;
using AntBoxFrontEnd.Services.AntBoxes;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class AntBoxController : Controller
    {

        private const int RANGE_HOURS_TO_TASK = 2;

        [HttpPost]
        public ActionResult UpdateAjax(string id, string name, string description)
        {
            var abs = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            AntBoxUpdateRequest updateOptions = new AntBoxUpdateRequest()
            {
                name = name,
                Description = description
            };
            bool result = false;
            try
            {
                result = abs.UpdateAntBox(updateOptions, id);
                return Json(new { success = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PaginationAjax(string idPagination, int page)
        {
            var servicio = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            PaginationAntBoxes result = new PaginationAntBoxes();
            try
            {
                result = servicio.ListAntBoxes(((CustomerResponse)Session["customer"]).Id, AntBoxStatusEnum.Defualt, page, idPagination);
            }
            catch (Exception ex)
            {
                
            }
            if (result == null)
            {
                result = new PaginationAntBoxes();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SolicitarAntbox (string worker, string address, string fecha, string hora, string referencia, string folioEntrega, string status, string idantbox, 
            string antboxserial, string id, List<antboxssolicitud> antboxs, List<antboxssolicitud> antboxsdeposit, List<antboxssolicitud> antboxsstore )
        {
            bool isTaskDeliveryCreated = false;
            try
            {
                if (id != null)
                {
                    var t = new TaskUpdateOptions
                    {
                        State = "cancelled"
                    };

                    var ts = new TaskService(ServiceConfiguration.GetApiKey());

                    var result = ts.UpdateTask(t, id);

                    if (result)
                    {
                        if (status == "inhouse")
                        {
                            isTaskDeliveryCreated = CreatePickupTask(fecha, hora, address, worker, folioEntrega);
                        }
                        else
                        {
                            isTaskDeliveryCreated = CreateDeliveryTask(fecha, hora, address, worker, folioEntrega);
                        }
                    }
                }
                else
                {
                    if (antboxsdeposit != null)
                    {
                        isTaskDeliveryCreated = CreatePickupTask(fecha, hora, address, worker, folioEntrega);
                    }
                    if (antboxsstore != null)
                    {
                        var folioRecoleccion = CheckOutBox(worker, antboxsstore, address);
                        if (!string.IsNullOrEmpty(folioRecoleccion))
                        {
                            isTaskDeliveryCreated = CreateDeliveryTask(fecha, hora, address, worker, folioRecoleccion);
                        }
                    } if (antboxs != null)
                    {
                        isTaskDeliveryCreated = CreateDeliveryTask(fecha, hora, address, worker, folioEntrega);
                    }
                }
            } catch (Exception ex)
            {
                //error
            }
            
            return Json(new { success = isTaskDeliveryCreated }, JsonRequestBehavior.AllowGet);
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

        private DateTime GetDateFromString(string date, string time)
        {
            int hour = 0;
            int.TryParse(time.Substring(0, 2), out hour);

            var dateForm = Convert.ToDateTime(date);

            var dateResult = DateHelpers.AppendTimeToDate(dateForm, new TimeSpan(hour, 0, 0));

            return dateResult;
        }

        private string CheckOutBox(string workerid, List<antboxssolicitud> antboxs)
        {
            var ids = new List<AntBoxObjectCheckout>();
            foreach (var item in antboxs)
            {
                ids.Add(new AntBoxObjectCheckout
                {
                    Id = item.id,
                    Serial = item.serial
                });
            }

            var order = new AntBoxRequestOptions()
            {
                Antboxs = ids,

                Customer_id = ((CustomerResponse)Session["customer"]).Id,

                Worker_id = workerid
            };

            var serv = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            var res = serv.CreateAntBoxesStore(order);

            return res;

        }

        public class antboxssolicitud
        {
            public string id { get; set; }
            public string serial { get; set; }
        }
    }
}