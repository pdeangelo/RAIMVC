using Microsoft.AspNet.Identity;
using RAI_MVC.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Security
{
    public class ApplicationUserManager
    {
        public ApplicationUserManager(IUserStore<AppUser> userStore)
       : base(userStore)
        {
        }
    }
}