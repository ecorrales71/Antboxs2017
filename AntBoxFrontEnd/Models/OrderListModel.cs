using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
using AntBoxFrontEnd.Services.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class OrderListModel
    {
        
        public PaginationOrder Orders { get; set; }

        public string Name { get; set; }

        public string Pedido { get; set; }

        public string Codigo { get; set; }

        public string Registro { get; set; }

        public string Recoleccion { get; set; }

        public string Entrega { get; set; }

        public string Status { get; set; }

        public int? Page { get; set; }
    }
}