using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RAI_MVC
{
    public class Context : DbContext
    {
        public Context()
        {
            this.Database.Connection.ConnectionString = "data source = 52.7.206.108; initial catalog = RAI_Test; user id = CashFlowMgr; password = p@$$p0rt; multipleactiveresultsets = True; application name = EntityFramework";
        }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Investor> Investors { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

           
        }
    }
}

