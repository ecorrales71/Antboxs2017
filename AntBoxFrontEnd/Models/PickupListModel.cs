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
    public class PickupListModel
    {

        public PaginationPickup Pickups { get; set; }

        public string Name { get; set; }

        public string Antboxs { get; set; }

        public string Operador { get; set; }

        public string Tipo { get; set; }

        public string Solicitud { get; set; }

        public string Recoleccion { get; set; }

        public string Horario { get; set; }

        public string Status { get; set; }


    }
}