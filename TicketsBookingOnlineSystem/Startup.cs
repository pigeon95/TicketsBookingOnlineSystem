using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Security.Claims;
using System.Web.Helpers;

[assembly: OwinStartupAttribute(typeof(TicketsBookingOnlineSystem.Startup))]
namespace TicketsBookingOnlineSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            /*
            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AppId = "xxx",//deleted
                AppSecret = "xxx",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = async ctx => {
                        
                    }
                }
            });
            */
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

    }
}
