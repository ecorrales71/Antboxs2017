using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Boxes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AntBoxFrontEnd.Entities
{
    public class AntBox
    {

        public string Model { get; set; }

        public string Description { get; set; }

        public string Sizes { get; set; }

        public decimal Price { get; set; }
        
        public decimal Secure { get; set; }

        public string Label { get; set; }

        public string Status { get; set; }

    }

    public class LineOrder
    {
        public string Model { get; set; }

        public string Description { get; set; }

        public string Sizes { get; set; }

        public decimal Price { get; set; }

        public decimal Secure { get; set; }

        public int Quantity { get; set; }

        public string Label { get; set; }

        [DataType(DataType.Currency)]
        public decimal LineTotal { get; set; }
    }


}