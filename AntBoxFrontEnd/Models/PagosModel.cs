using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.BillingAddress;
using AntBoxFrontEnd.Services.Code;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.CustomerService;
using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.Rules;
using AntBoxFrontEnd.Services.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class PagosModel
    {
        public List<CardObject> Cards { get; set; }

        public List<BillingAddressResponse> Address { get; set; }

        public List<ChargeResponse> Payments { get; set; }
    }
}