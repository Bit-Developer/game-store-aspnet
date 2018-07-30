using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GameStore.WebUI.Startup))]
namespace GameStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}