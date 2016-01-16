using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Lohas.Web.Startup))]
namespace Lohas.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
