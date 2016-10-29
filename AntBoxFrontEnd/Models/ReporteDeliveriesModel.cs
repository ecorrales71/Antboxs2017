using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Coupon;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
using AntBoxFrontEnd.Services.Payments;
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
    public class ReporteDeliveriesModel
    {
        public PaginationDelivery Deliveries { get; set; }

        public string From { get; set; }
        
        public string To { get; set; }
        
        public string Status { get; set; }

        public int? Page { get; set; }

    }
}