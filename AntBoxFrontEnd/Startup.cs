using Microsoft.Owin;
using Owin;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services.Login;
using AntBoxFrontEnd.Services.Customer;
using AntBoxFrontEnd.Services;

[assembly: OwinStartupAttribute(typeof(AntBoxFrontEnd.Startup))]
namespace AntBoxFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // CreateCustomer();
            Login();
           // Requestor.GetHovaToken();

        }



        private void Login()
        {

            var usr = new AntBoxFrontEnd.Services.Login.LoginCreateOptions
            {
                Email = "emartinez@greenrivercg.com.mx",
                Password = "123"
            };


            LoginService ls = new LoginService(ServiceConfiguration.GetApiKey());

            string id = ls.HovaLogin(usr);
            

        }


        private void CreateCustomer()
        {
            var cus = new AntBoxFrontEnd.Services.Customer.CustomerRequestOptions
            {
                Email = "prueba@prueba.com",
                Name = "Prueba nombre",
                LastName = "Prueba Apellido paterno",
                Lastname2 = "Prueba Apellido materno",
                Mobile_phone = "55555555555",
                Password = "258abc"                
            };

            var ser = new CustomerServices(ServiceConfiguration.GetApiKey());

            var res = ser.CreateCustomer(cus);
            

        }

    }
}
