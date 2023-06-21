using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RAI_MVC
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var loanStatus1 = new LoanStatus()
            {
                LoanStatusName = "1 - Underwriting"
            };
            var client1 = new Client()
            {
                ClientName = "Client 1"
            };
            var state = new State()
            {
                State1 = "NY"
            };
            var investor = new Investor()
            {
                InvestorName = "Investor 1"
            };
            var dwellingType = new DwellingType()
            {
                DwellingType1 = "Dwelling Type 1"
            };
            var entity = new Entity()
            {
                EntityName = "Entity 1"
            };
            var loanType = new LoanType()
            {
                LoanTypeName = "Loan Type 1"
            };
            var loan1 = new Loan()
            {
                LoanNumber = "Loan 1"
            };
            loan1.LoanStatus = loanStatus1;
            loan1.Investor = investor;
            loan1.Entity = entity;
            loan1.DwellingType = dwellingType;
            loan1.Client = client1;
            loan1.State = state;
            loan1.LoanType = loanType;

            context.Loans.Add(loan1);

            var role = new Role()
            {
                RoleName = "Administrator"
            };
            var user = new User()
            {
                UserName = "Paul",
                Role = role
            };
            context.SaveChanges();


        }
    }
}
