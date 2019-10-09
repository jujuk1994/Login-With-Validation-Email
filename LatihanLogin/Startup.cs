using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LatihanLogin.Startup))]
namespace LatihanLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
