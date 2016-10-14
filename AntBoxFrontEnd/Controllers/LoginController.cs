using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AntBoxLoginAjax(string username, string password)
        {
            //if (ModelState.IsValid)w
            //{
            var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
            {
                Email = username,
                Password = password
            };

            LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

            string id = ls.HovaLogin(usr);

            UserServices us = new UserServices(ServiceConfiguration.GetApiKey());

            UserResponse admin = us.SearchUser(id);

            if (admin != null)
            {
                Session["admin"] = admin;
                return Json(new { success = true, user = admin }, JsonRequestBehavior.AllowGet);
                //return Json(new { success = true, user = customer }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, user = admin }, JsonRequestBehavior.AllowGet);
            }
            //}
            //return RedirectToAction("Index", "Home");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
