using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MatchboxService.Startup))]

namespace MatchboxService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}