using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            if (Session["customer"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            return View(Session["customer"]);
        }

        public ActionResult Cuenta()
        {
            return View();
        }

        public ActionResult Direcciones()
        {
            if (Session["customer"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            CustomerResponse customer = (CustomerResponse)Session["customer"];

            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.ListAddresses(customer.Id);

            return View(res);
        }

        public ActionResult Antboxs()
        {
            return View();
        }
        public ActionResult Movimientos()
        {
            return View();
        }
        public ActionResult Ordenar()
        {
            return View();
        }
        public ActionResult Pagos()
        {
            return View();
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
    }
}
