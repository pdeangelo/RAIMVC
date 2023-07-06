using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RAI_MVC.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.App_Start
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext(() => new Context());
            ////app.CreatePerOwinContext<AppUser>(AppUser.Create);
            //app.CreatePerOwinContext<RoleManager<AppRole>>((options, context) =>
            //    //new RoleManager<AppRole>(
            //    //    new RoleStore<AppRole>(context.Get<Context>())));

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/RAI/Login"),
            //});
        }
    }
}