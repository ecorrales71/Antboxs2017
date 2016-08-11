using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Acerca de.";

            return View();
        }

        public ActionResult ComoFunciona()
        {
            ViewBag.Message = "¿Cómo Funciona?";

            return View();
        }

        public ActionResult QueDeboHacer()
        {
            ViewBag.Message = "¿Qué debo hacer?";

            return View();
        }

        public ActionResult Precios()
        {
            ViewBag.Message = "Precios";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pagina de contacto.";

            return View();
        }
    }
}