using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TicketsBookingOnlineSystem.Startup))]
namespace TicketsBookingOnlineSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
