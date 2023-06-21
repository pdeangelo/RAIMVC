using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Diagnostics;

namespace RAI_MVC.Repository
{
    public class LoanRepository
    {
        public IList<Loan> GetLoans()
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
        public Loan GetLoan(int loanID)
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
        public static void AddLoan(Loan loan)
        {
            using (Context context = GetContext())
            {
                context.Loans.Add(loan);

                if (loan.Client != null && loan.Client.ClientID > 0)
                {
                    context.Entry(loan.Client).State = EntityState.Unchanged;
                }

                if (loan.DwellingType != null && loan.DwellingType.DwellingTypeID > 0)
                {
                    context.Entry(loan.DwellingType).State = EntityState.Unchanged;
                }

                if (loan.Entity != null && loan.Entity.EntityID > 0)
                {
                    context.Entry(loan.Entity).State = EntityState.Unchanged;
                }

                if (loan.LoanStatus != null && loan.LoanStatus.LoanStatusID > 0)
                {
                    context.Entry(loan.LoanStatus).State = EntityState.Unchanged;
                }

                if (loan.LoanType != null && loan.LoanType.LoanTypeID > 0)
                {
                    context.Entry(loan.LoanType).State = EntityState.Unchanged;
                }

                if (loan.State != null && loan.State.StateID > 0)
                {
                    context.Entry(loan.State).State = EntityState.Unchanged;
                }

                context.SaveChanges();
            }
        }
        public static void UpdateLoan(Loan loan)
        {
            using (Context context = GetContext())
            {
                context.Loans.Attach(loan);
                //comicBookEntry.Property("IssueNumber").IsModified = false;

                context.SaveChanges();
            }
        }
        public static void DeleteLoan(int loanID)
        {
            using (Context context = GetContext())
            {
                var loan = new Loan() { LoanID = loanID };
                context.Entry(loan).State = EntityState.Deleted;

                context.SaveChanges();
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
                var loanTypes = context.LoanType.ToList();
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
        public SelectList GetUsers()
        {
            using (Context context = GetContext())
            {
                var users = context.User.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (User user in users)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = user.UserID.ToString(),
                        Text = user.UserName
                    });
                }
                return new SelectList(list, "Value", "Text");
            }

        }

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