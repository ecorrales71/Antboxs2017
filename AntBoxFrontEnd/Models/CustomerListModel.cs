using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class CustomerListModel
    {
        
        public PaginationCustomerResponse Customers { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Rfc_id { get; set; }

        public int? Antboxs { get; set; }

        public string Status { get; set; }


    }
}