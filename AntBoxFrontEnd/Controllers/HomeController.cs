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

        public ActionResult Contacto()
        {
            ViewBag.Message = "Pagina de contacto.";

            return View();
        }

        public ActionResult PreguntasFrecuentes()
        {
            ViewBag.Message = "Preguntas frecuentes.";

            return View();
        }


        public ActionResult AgendarEntregas()
        {
            ViewBag.Message = "Agendar entregas.";

            return View();
        }

        public ActionResult CrearCuenta()
        {
            ViewBag.Message = "Agendar entregas.";

            return View();
        }

        public ActionResult OrdenCompleta()
        {
            ViewBag.Message = "Agendar entregas.";

            return View();
        }



    }
}