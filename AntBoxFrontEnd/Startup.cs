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


            //ListCustomer();
            //CreateCustomer();
            //UpdateCustomer();
            // Login();
            // Requestor.GetHovaToken();
            ListAddresses();


        }



        private void Login()
        {

            var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
            {
                Email = "prueba@prueba.com",
                Password = "258abc"
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

        private void ListAddresses()
        {
            var l = new AddressService(ServiceConfiguration.GetApiKey());

            var res = l.ListAddresses("ag6hhjauatyy127jas");


        }


    }
}
