using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AntBoxFrontEnd.Startup))]
namespace AntBoxFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            

        }
    }
}
