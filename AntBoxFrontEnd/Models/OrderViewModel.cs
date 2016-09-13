using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Models
{
    public class OrderViewModel
    {

        public List<AntBoxAddressViewModel> Addresses { get; set; }

        public string PickupAddressId { get; set; }

        public string DeliveryAddressId { get; set; }

        public bool Service_time_Pickup { get; set; }
        
        public AntBoxAddressViewModel NewAddress { get; set; }

        public DateTime FromPickup { get; set; }

        public DateTime ToPickup { get; set; }


        public DateTime FromDelivery { get; set; }

        public DateTime ToDelivery { get; set; }



        public AntBoxesViewModel Boxes { get; set; }


    }
}