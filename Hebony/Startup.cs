using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hebony.Startup))]
namespace Hebony
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
