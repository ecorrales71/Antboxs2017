using Microsoft.Owin;
using Owin;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services.Address;
using AntBoxFrontEnd.Services;

[assembly: OwinStartupAttribute(typeof(AntBoxFrontEnd.Startup))]
namespace AntBoxFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            // CreateAddress();

            // ListCustomer();
            //CreateCustomer();
            //  UpdateCustomer();
            //   Login();
            // Requestor.GetHovaToken();
            //UpdateAddress();
            // SearchAddresses();
            DeleteAdress();
            ListAddresses();


        }



        private void Login()
        {

            var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
            {
                Email = "prueba4@prueba.com",
                Password = "44abc"
            };


            LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

            string id = ls.HovaLogin(usr);
            

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

            var res = ser.UpdateCustomer(cus, "e5bb4f5290ab4fb8b75a84046a09a921");

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
                Alias = "oficina"
                ,Customer_id = "b3c7fcada2e8473491a7d12e302a3e31"
                ,Delegation = "milpa alta"
                ,External_number = "5001"
                ,Neighborhood = "ahuehuetes"
                ,State = "ciudad de méxico"
                ,Street = "avenida toltecas"
                ,City = "México"
                ,Country = "México"
                ,Rfc_id = "RFC 10455"
                ,References = "referencias direccion nueva"
                ,Zipcode = "07878"
                ,Internal_number = "4b"
            };
            var a = new AddressService(ServiceConfiguration.GetApiKey());

            var ad = a.CreateAddress(address);


        }


        private void UpdateAddress()
        {
            var address = new AddressUpdateOptions
            {
                Alias = "nueva dir updated"
                
                ,
                Delegation = "miguel hidalgo"
                ,
                External_number = "5 updated"
                ,
                Neighborhood = "colonia updated"
                ,
                State = "México updated"
                ,
                Street = "Calle updated"
                , Zipcode = "04577"
                ,City = "Ciudad updated"
                ,
                Country = "México updated"
                ,
                References = "Referencia nueva updated"
                ,
                Rfc_id = "RFC updated"


            };
            var a = new AddressService(ServiceConfiguration.GetApiKey());

            var ad = a.UpdateAddress(address, "a541f8b2151e4920a927d00cfb9903d3");


        }

        private void ListAddresses()
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.ListAddresses("b3c7fcada2e8473491a7d12e302a3e31");


        }


        private void SearchAddresses()
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.SearchAddress("a541f8b2151e4920a927d00cfb9903d3");


        }


        private void DeleteAdress()
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.DeleteAddress("ef6c30d75ea54e609428bf958e64ff71");

        }


    }
}
