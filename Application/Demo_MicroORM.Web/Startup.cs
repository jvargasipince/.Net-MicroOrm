using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo_MicroORM.Web.Startup))]
namespace Demo_MicroORM.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
