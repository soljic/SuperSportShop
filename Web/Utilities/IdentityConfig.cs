using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Web.Utilities
{
    public static class IdentityConfig
    {
        public static void RegisterIdentity(IAppBuilder app)
        {
            // Konfiguracija Identity sistema
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }

}