using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KN_Order_Storage.Startup))]
namespace KN_Order_Storage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
