using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetaGamer.Startup))]
namespace BetaGamer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
