using AntBoxFrontEnd.Services.Payments;
using AntBoxFrontEnd.Services.Tasks;
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

        public string WorkerIdPickup { get; set; }

        public string WorkerIdDelivery { get; set; }

        public List<Schedules> HorariosEntrega { get; set; }

        public List<Schedules> HorariosRecoleccion { get; set; }

        public AntBoxAddressViewModel NewAddress { get; set; }

        public DateTime PickupDate{ get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool ContactByTel { get; set; }

        public bool ContactByEmail { get; set; }

        public List<CardObject> Cards { get; set; }


        public AntBoxesViewModel Boxes { get; set; }


    }
}