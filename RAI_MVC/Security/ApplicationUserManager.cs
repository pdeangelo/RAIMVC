using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RAI_MVC.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Security
{
    public class ApplicationUserManager : UserManager<AppUser>
    {
        public ApplicationUserManager(IUserStore<AppUser> store)
       : base(store)
        {
        }

        // this method is called by Owin therefore this is the best place to configure your User Manager
        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(
                new UserStore<AppUser>(context.Get<Context>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}