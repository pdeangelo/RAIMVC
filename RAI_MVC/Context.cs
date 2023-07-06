using Microsoft.AspNet.Identity.EntityFramework;
using RAI_MVC.Classes;
using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RAI_MVC
{
    public class Context : IdentityDbContext<AppUser>
    {

        public Context() : base("RAINew")
        {
            //Database.SetInitializer(new DatabaseInitializer());
            //Database.SetInitializer<Context>(null);
        }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanStatus> LoanStatus { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<DwellingType> DwellingType { get; set; }
        public DbSet<Entity> Entity { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Investor> Investor { get; set; }
        public DbSet<LoanType> LoanType { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
    }
}

