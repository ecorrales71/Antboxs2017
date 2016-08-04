using Microsoft.Owin;
using System;
using Owin;
using AntBoxFrontEnd.Infrastructure;
using AntBoxFrontEnd.Services;

[assembly: OwinStartupAttribute(typeof(AntBoxFrontEnd.Startup))]
namespace AntBoxFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var serv = new ServicesTest();

            serv.Start();

        }




    }
}
