using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YodaProject.Startup))]
namespace YodaProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
