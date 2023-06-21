using RAIModel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace RAIModel.Repositories
{
    class Repository
    {
        public static IList<Loan> GetLoans()
        {
            using (Context context = GetContext())
            {
                return context.Loans
                    .Include(l => l.State)
                  .Include(l => l.Client)
                  .Include(l => l.LoanStatus)
                  .Include(l => l.Entity)
                  .Include(l => l.Investor)
                  .Include(l => l.DwellingType)
                  .Include(l => l.LoanType)
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public static Loan GetLoan(int loanID)
        {
            using (Context context = GetContext())
            {
                 return context.Loans
                  .Include(l => l.State)
                  .Include(l => l.Client)
                  .Include(l => l.LoanStatus)
                  .Include(l => l.Entity)
                  .Include(l => l.Investor)
                  .Include(l => l.DwellingType)
                  .Include(l => l.LoanType)
                    .Where(cb => cb.LoanID == loanID)
                    .SingleOrDefault();
            }
        }
        public SelectList GetInvestors()
        {
            using (Context context = GetContext())
            {
                var investors = context.Investor.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (Investor investor in investors)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = investor.InvestorID.ToString(),
                        Text = investor.InvestorName
                    });
                }
                return new SelectList(list, "Value", "Text");
            }


        }
        public SelectList GetTypes()
        {
            using (Context context = GetContext())
            {
                var loanTypes = context.LoanType .ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (LoanType loanType in loanTypes)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = loanType.LoanTypeID.ToString(),
                        Text = loanType.LoanTypeName
                    });
                }
                return new SelectList(list, "Value", "Text");
            }
        }
        public SelectList GetEntities()
        {
            using (Context context = GetContext())
            {
                var entities = context.Entity.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (Entity entity in entities)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = entity.EntityID.ToString(),
                        Text = entity.EntityName
                    });
                }
                return new SelectList(list, "Value", "Text");
            }

        }
        public SelectList GetStatus()
        {
            using (Context context = GetContext())
            {
                var statuss = context.LoanStatus.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (LoanStatus status in statuss)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = status.LoanStatusID.ToString(),
                        Text = status.LoanStatusName
                    });
                }
                return new SelectList(list, "Value", "Text");
            }

        }
        public SelectList GetStates()
        {
            using (Context context = GetContext())
            {
                var states = context.State.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (State state in states)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = state.StateID.ToString(),
                        Text = state.State1
                    });
                }
                return new SelectList(list, "Value", "Text");
            }

        }
        //public SelectList GetUsers()
        //{
        //    using (Context context = GetContext())
        //    {
        //        var users = context.Users.ToList();
        //        List<SelectListItem> list = new List<SelectListItem>();
        //        foreach (User user in users)
        //        {
        //            list.Add(new SelectListItem()
        //            {
        //                Value = user.UserID.ToString(),
        //                Text = user.UserName
        //            });
        //        }
        //        return new SelectList(list, "Value", "Text");
        //    }

        //}

        public SelectList GetClients()
        {
            using (Context context = GetContext())
            {
                var clients = context.Client.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (Client client in clients)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = client.ClientID.ToString(),
                        Text = client.ClientName
                    });
                }
                return new SelectList(list, "Value", "Text");
            }


        }

        /// <summary>
        /// Private method that returns a database context.
        /// </summary>
        /// <returns>An instance of the Context class.</returns>
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

    }
}
