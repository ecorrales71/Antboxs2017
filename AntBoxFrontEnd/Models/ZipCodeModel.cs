using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Coupon;
using AntBoxFrontEnd.Services.User;
using AntBoxFrontEnd.Services.Zipcodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class ZipCodeModel
    {
        public PaginationZipCodesResponse Zipcodes { get; set; }

        public string Codigo { get; set; }
        
        public string Estado { get; set; }
        
        public string Municipio { get; set; }
        
        public string Colonia { get; set; }
        
        public string Registro { get; set; }

        public bool? Status { get; set; }

        public int? Page { get; set; }

    }
}