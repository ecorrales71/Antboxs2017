using AntBoxFrontEnd.Entities;
using AntBoxFrontEnd.Services.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class AntBoxesViewModel
    {
        
        public List<AntBox> ActiveAntBoxes { get; set; }

        public List<LineOrder> Order { get; set; }

        [DataType(DataType.Currency)]
        public decimal OrderTotal { get; set; }

        [DataType(DataType.Currency)]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        [DataType(DataType.Currency)]
        public decimal Iva { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

       


    }
}