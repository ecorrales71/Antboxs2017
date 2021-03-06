﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.Tasks;
using AntBoxFrontEnd.Services.Zipcodes;
using System.Diagnostics;
using AntBoxFrontEnd.Services.Worker;
using AntBoxFrontEnd.Services.Boxes;
using AntBoxFrontEnd.Services.AntBoxes;

namespace AntBoxFrontEnd.Services
{
    public class ServicesTest
    {
        
        public void Start()
        {
           //CreateCustomer();

            //SearchZip();

            var idcustomer = Login();

            //getSchedules();

            // Debug.WriteLine(idcustomer);

            //  CreateAddress(idcustomer,4);


            //  CreateValidAddress(idcustomer);

            ListaAntBoxes(idcustomer);

            //var idAddress = ListAddresses(idcustomer, 1)
            //                    .Where(a => a.Alias.ToUpper().Contains("VALIDA")).FirstOrDefault();

            //Debug.WriteLine(idAddress.ToString());

            //var address = SearchAddresses(idAddress.Id);

            // Debug.WriteLine(address.Street);

            // UpdateAddress(idAddress);

            // var addressNew = SearchAddresses(idAddress);

            // Debug.WriteLine(addressNew.ToString());

            var worker = GetWorker();

            var listabox = ListBoxes(idcustomer);

            var AntBox = listabox
                        .OrderBy(a => a.Id)
                        .FirstOrDefault();

            Debug.WriteLine(AntBox.Id);

            //var orderid = CreateAntBoxesOrder(AntBox.Id, idcustomer, worker);

            //var restask = CreateTask(idcustomer, address.Id, orderid, worker);


            var tasks = ListarTareas(worker);

            //Debug.WriteLine("Se creo Tarea: " + restask);

            //var box = CreateBoxes(idcustomer, 2);
            //Debug.WriteLine(box);

           

            //listabox.ForEach(x =>
            //{
            //    Debug.WriteLine(x.Id);
            //    DeleteBox(x.Id);
            //});

            //var boxtest = SearchBox(idbox.Id);
            //Debug.WriteLine(idbox);

            //Debug.WriteLine("Se actualizo box" + UpdateBox(idbox.Id));

            //SearchBox(idbox.Id);

            //var delbox = DeleteBox(idbox.Id);


            //CreateWorker(2);





        }


        private string  Login()
        {

            var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
            {
               // Email = "prueba4@prueba.com",
                //Password = "44abc"
                Email = "emcastaneda@greenrivercg.com.mx",
                Password = "87654321"
            };


            LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

            string id = ls.HovaLogin(usr);

            return id;

        }


        private void CreateCustomer()
        {
            var cus = new AntBoxFrontEnd.Services.Customer.CustomerRequestOptions
            {
                Email = "emcastaneda@greenrivercg.com.mx",
                Name = "Eric",
                LastName = "Martinez",
                Lastname2 = "Castañeda",
                Mobilephone = "5543434343",
                Phone = "5546464646",
                Password = "87654321",
                Username = "emcastaneda",
                Status = true
                
            };

            var ser = new CustomerServices(ServiceConfiguration.GetApiKey());

            var res = ser.CreateCustomer(cus);

        }

        public PaginationAntBoxes ListaAntBoxes(string customerid)
        {
            var service = new AntBoxesServices(ServiceConfiguration.GetApiKey());


            var res = service.ListAntBoxes(customerid, AntBoxStatusEnum.Defualt, 1);

            return res;
        }


        private void UpdateCustomer()
        {
            var cus = new AntBoxFrontEnd.Services.Customer.CustomerUpdateOptions
            {
                Email = "prueba4@prueba.com",
                Name = "Prueba nombre4 modificado",
                LastName = "Prueba Apellido paterno4 mod",
                Lastname2 = "Prueba Apellido materno4",
                Mobile_phone = "5523232323"
            };

            var ser = new CustomerServices(ServiceConfiguration.GetApiKey());

            var res = ser.UpdateCustomer(cus, "30993450aa834a35af67bfe6b978059a");

        }

        private void ListCustomer()
        {
            var l = new CustomerServices(ServiceConfiguration.GetApiKey());

            var res = l.SearchCustomer("e5bb4f5290ab4fb8b75a84046a09a921");


        }


        private void CreateAddress(string idcustomer , int numero)
        {
            for(int i = 0; i < numero; i++)
            {
                var address = new AddressRequestOptions
                {
                    Alias = "nueva prueba " + i,
                    Customer_id = idcustomer,
                    Delegation = "Delegacion " + i,
                    External_number = "NumExt " + i,
                    Neighborhood = "Colonia " +i,
                    State = "ciudad de méxico",
                    Street = "Calle " + i,
                    City = "México",
                    Country = "México",
                    Rfc_id = "RFC" + i,
                    References = "referencias direccion " + i,
                    Zipcode = "01000",
                    Internal_number = "IntNum "+ i
                };
                var a = new AddressService(ServiceConfiguration.GetApiKey());

                var ad = a.CreateAddress(address);


            }

           


        }

        private void CreateValidAddress(string idcustomer)
        {
            var address = new AddressRequestOptions
            {
                Alias = "Calle alicia" ,
                Customer_id = idcustomer,
                Delegation = "Álvaro Obregon",
                External_number = "105",
                Internal_number = "402",
                Neighborhood = "Sears Roebuck",
                State = "Ciudad de México",
                Street = "Alicia",
                City = "Ciudad de México",
                Country = "México",
                Rfc_id = "RFC" ,
                References = "Portón Blanco, sin número visble",
                Zipcode = "01120"
            };
            var a = new AddressService(ServiceConfiguration.GetApiKey());

            var ad = a.CreateAddress(address);
            




        }
        private void UpdateAddress(string idaddress)
        {
            var address = new AddressUpdateOptions
            {
                Alias = "nueva prueba  updated"

                ,
                Delegation = "miguel hidalgo updated"
                ,
                External_number = "5 updated"
                ,
                Neighborhood = "colonia updated"
                ,
                State = "México updated"
                ,
                Street = "Calle updated"
                ,
                Zipcode = "00000"
                ,
                City = "Ciudad updated"
                ,
                Country = "México updated"
                ,
                References = "Referencia nueva updated"
                ,
                Rfc_id = "xxxxx updated"



            };
            var a = new AddressService(ServiceConfiguration.GetApiKey());

            var ad = a.UpdateAddress(address, idaddress);


        }

        private List<AddressResponse> ListAddresses(string idcustomer, int currentpage, string idPagination = null)
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.ListAddresses(idcustomer, currentpage, idPagination);

            return res;

        }


        private AddressResponse SearchAddresses(string id)
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.SearchAddress(id);

            return res;
        }


        private void DeleteAdress()
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.DeleteAddress("ef6c30d75ea54e609428bf958e64ff71");

        }

        public string GetWorker()
        {
            var ts = new TaskService(ServiceConfiguration.GetApiKey());

            var worker = ts.ListSchedules(DateTime.Now).FirstOrDefault().worker;
            return worker;
        }


        public bool CreateTask(string idcustomer, string idAddress, string folio, string worker)
        {
            var from = new DateTime(2016, 9, 25, 11, 0, 0);
            var to = from.AddHours(2);

            var ts = new TaskService(ServiceConfiguration.GetApiKey());
            var t = new TaskRequestOption
            {
                Address_id = idAddress,
                customer_id = idcustomer,
                from = DateHelpers.ToUnixTime(from),
                To = DateHelpers.ToUnixTime(to),
                Worker_id = worker,
                Folio = folio,
                Type = ts.Type_Pickup,
                Service_time = true
            };

            

            return ts.CreateTask(t);
        }

        public string CreateAntBoxesOrder(string idbox, string customer, string worker, string address)
        {
            var order = new AntBoxRequestOptions()
            {
                Checkouts =  new CheckOut[] {new CheckOut ()
                    {
                        Box_id = idbox,
                        Quantity = 3
                    }
                },
                
                Customer_id = customer,
                
                Worker_id = worker,

                Address_id = address
            };

            var serv = new AntBoxesServices(ServiceConfiguration.GetApiKey());

            var res = serv.CreateAntBoxes(order);


            return res;


        }



        public void SearchZip()
        {

            string zipcode = "01000";

            var ser = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var ans = ser.SearchZipCode(zipcode);

        }


        private void CreateWorker(int num)
        {
            //for(int i = 0; i< num; i++)
            //{
            //    var cus = new WorkerRequestOption
            //    {                    
            //        Email = "prueba"+ i+"@prueba.com",
            //        Name = "Prueba nombre " +i,
            //        Phone = "Prueba Apellido paterno " + i,
            //        Capacity = 1,
            //        Password = "123456",
            //        Status = true                    
            //    };

            //    var ser = new WorkerService(ServiceConfiguration.GetApiKey());

            //    var res = ser.CreateWorker(cus);
            //}
        }


        private bool CreateBoxes(string id,int num)
        {
            var caja1 = new BoxesRequestOptions
            {
                Label = "Antboxs Chica",
                Model = "antboxch",
                Price = 150,
                Secure = 1500,
                Registered_by = id,
                Size = "27\" x 17\" x 12\"",
                Status = true
            };

            var caja2 = new BoxesRequestOptions
            {
                Label = "Antboxs Mediana",
                Model = "antboxmd",
                Price = 200,
                Secure = 1500,
                Registered_by = id,
                Size = "27\" x 17\" x 12\"",
                Status = true
            };


                var ser = new BoxesService(ServiceConfiguration.GetApiKey());

            var res = ser.CreateBoxes(caja1);
            var res2 = ser.CreateBoxes(caja2);

            return true;
        }



        private List<BoxesResponse> ListBoxes(string idcustomer)
        {
            var l = new BoxesService(ServiceConfiguration.GetApiKey());

            var res = l.ListBoxes(StatusBoxes.Active);

            return res;

        }

        private bool DeleteBox(string id)
        {
            var l = new BoxesService(ServiceConfiguration.GetApiKey());

            return l.DeleteBoxes(id);

        }

        private BoxesResponse SearchBox(string id)
        {
            var l = new BoxesService(ServiceConfiguration.GetApiKey());

            var res = l.SearchBox(id);

            return res;
        }

        private bool UpdateBox(string id)
        {
            var l = new BoxesService(ServiceConfiguration.GetApiKey());

            var cus = new BoxesUpdateOptions
            {
                Label = "ALIAS updated",
                Model = "modelo  updated",
                Price = 301,
                Secure = 750,
                Registered_by = id,
                Size = "Size  updated"
            };

            var res = l.UpdateBox(cus,id);

            return res;
        }


        public void getSchedules()
        {
            var serv = new TaskService(ServiceConfiguration.GetApiKey());

            DateTime d = DateTime.Now;

            var dd = d.ToString("yyyy-MM-dd");

            var sc = serv.ListSchedules(dd) ;


        }


        public ListTask ListarTareas(string workerid)
        {
            var servicio = new TaskService(ServiceConfiguration.GetApiKey());

            var res = servicio.ListTaskByWorker(workerid);

            return res;

        }




    }
}