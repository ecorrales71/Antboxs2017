using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Zipcodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AntBoxFrontEnd.Controllers
{
    public class ZipCodeController : Controller
    {
        //
        // GET: /Code/
        public ActionResult CreateZipCode(ZipCodeRequestOptions modelCode)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                List<ZipCodeRequestOptions> zipcodes = new List<ZipCodeRequestOptions>();
                zipcodes.Add(modelCode);

                string json = new
                            System.Web.Script.Serialization.JavaScriptSerializer().Serialize(zipcodes);

                var ser = new ZipcodeService(ServiceConfiguration.GetApiKey());
                var res = ser.CreateZipCodes(json);
                return Json(new { success = res, responseText = "Codigo zip registrado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL REGISTRAR EL CÓDIGO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateZipCode(string id, string country, string state, string neighborhood, string delegation)
        {
            try
            {
                if (Session["admin"] == null)
                {
                    return Json(new { success = false, responseText = "Opción no permitida" }, JsonRequestBehavior.AllowGet);
                }

                ZipCodeUpdateOptions cu = new ZipCodeUpdateOptions
                {
                    State = state,
                    Neighborhood = neighborhood,
                    Delegation = delegation,
                };

                var couponService = new ZipcodeService(ServiceConfiguration.GetApiKey());

                var result = couponService.UpdateZipCode(cu, id);

                return Json(new { success = result, responseText = "Código zip actualizado" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                LogManager.Write(ex.Message, LogManager.Error);
                return Json(new { success = false, responseText = "OCURRIO UN ERROR AL ACTUALIZAR EL CÓDIGO" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteZipCode(string id)
        {
            var zipCodeService = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = zipCodeService.DeleteZipCode(id);


            if (result)
                return Json(new { success = result, responseText = "Se borro exitósamente el registro" }, JsonRequestBehavior.AllowGet);

            return Json(new { success = result, responseText = "Ocurrio un error al borrar el registro" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetZipCode(string id, string neighborhood)
        {
            var couponService = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var result = couponService.SearchZipCode(id, neighborhood);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    var zipCodeService = new ZipcodeService(ServiceConfiguration.GetApiKey());
                    string fname = "";
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Content/Admin/CSV/"), fname);
                        file.SaveAs(fname);

                        var csv = new List<string[]>(); // or, List<YourClass>
                        var lines = System.IO.File.ReadAllLines(fname);
                        List<ZipCodeRequestOptionsCsv> zipcodes = new List<ZipCodeRequestOptionsCsv>();
                        var count = 0;
                        foreach (string line in lines)
                        {
                            if (count > 0)
                            {
                                string[] linea = line.Split(',');
                                var item = new ZipCodeRequestOptionsCsv
                                {
                                    State = linea[0],
                                    Delegation = linea[1],
                                    Zipcode = linea[2],
                                    Neighborhood = linea[3],
                                    Latitude = linea[4],
                                    Longitude = linea[5]
                                };
                                zipcodes.Add(item);
                            }
                            count = count + 1;
                        }
                        string json = new
                            System.Web.Script.Serialization.JavaScriptSerializer().Serialize(zipcodes);

                        bool success = zipCodeService.CreateZipCodes(json);

                        return Json(success);
                    }
                    // Returns message that successfully uploaded  

                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }  
	}
}
