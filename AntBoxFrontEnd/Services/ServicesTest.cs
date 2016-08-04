using System;
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


namespace AntBoxFrontEnd.Services
{
    public class ServicesTest
    {
        
        public void Start()
        {
            SearchZip();

            //var idcustomer = Login();

            //var idAddress = ListAddresses(idcustomer)
            //                    .FirstOrDefault().Id;


            //var address = SearchAddresses(idAddress);
            

            //CreateTask(idcustomer, idAddress);
        }


        private string  Login()
        {

            var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
            {
                Email = "prueba4@prueba.com",
                Password = "44abc"
            };


            LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

            string id = ls.HovaLogin(usr);

            return id;

        }


        private void CreateCustomer()
        {
            var cus = new AntBoxFrontEnd.Services.Customer.CustomerRequestOptions
            {
                Email = "prueba4@prueba.com",
                Name = "Prueba nombre4",
                LastName = "Prueba Apellido paterno4",
                //Lastname2 = "Prueba Apellido materno4",
                Mobile_phone = "55333333333",
                Password = "44abc"
            };

            var ser = new CustomerServices(ServiceConfiguration.GetApiKey());

            var res = ser.CreateCustomer(cus);

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


        private void CreateAddress()
        {
            var address = new AddressRequestOptions
            {
                Alias = "oficina",
                Customer_id = "b3c7fcada2e8473491a7d12e302a3e31",
                Delegation = "milpa alta" ,
                External_number = "5001"
                ,
                Neighborhood = "ahuehuetes"
                ,
                State = "ciudad de méxico"
                ,
                Street = "avenida toltecas"
                ,
                City = "México"
                ,
                Country = "México"
                ,
                Rfc_id = "RFC 10455"
                ,
                References = "referencias direccion nueva"
                ,
                Zipcode = "07878"
                ,
                Internal_number = "4b"
            };
            var a = new AddressService(ServiceConfiguration.GetApiKey());

            var ad = a.CreateAddress(address);


        }


        private void UpdateAddress()
        {
            var address = new AddressUpdateOptions
            {
                Alias = "oficina updated"

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

            var ad = a.UpdateAddress(address, "30993450aa834a35af67bfe6b978059a");


        }

        private List<AddressResponse> ListAddresses(string idcustomer)
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.ListAddresses(idcustomer);

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


        public void CreateTask(string idcustomer, string idAddress)
        {
            var t = new TaskRequestOption
            {
                Address_id = idAddress
                ,customer_id = idcustomer
                ,from = DateHelpers.ToUnixTime(DateTime.Now)
                ,To = DateHelpers.ToUnixTime(DateTime.Now)
            };

            var ts = new TaskService(ServiceConfiguration.GetApiKey());

            var res = ts.CreateTask(t);
        }



        public void SearchZip()
        {

            string zipcode = "01000";

            var ser = new ZipcodeService(ServiceConfiguration.GetApiKey());

            var ans = ser.SearchZipCode(zipcode);

        }


    }
}