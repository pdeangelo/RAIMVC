using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RAI_MVC.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Security
{
    public class ApplicationSignInManager : SignInManager<AppUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
        {
        }
    }
}