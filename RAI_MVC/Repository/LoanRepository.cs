using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace RAI_MVC.Repository
{
    public class LoanRepository
    {
        public SelectList GetInvestors()
        {
            using (var context = new RAI_TestEntities5())
            {
                var investors = context.Investors.ToList();
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
            using (var context = new RAI_TestEntities5())
            {
                var loanTypes = context.LoanTypes.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (LoanType loanType in loanTypes)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = loanType.LoanTypeID.ToString(),
                        Text = loanType.LoanType1
                    });
                }
                return new SelectList(list, "Value", "Text");
            }
        }
        public SelectList GetEntities()
        {
            using (var context = new RAI_TestEntities5())
            {
                var entities = context.Entities.ToList();
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
            using (var context = new RAI_TestEntities5())
            {
                var statuss = context.Status.ToList();
                List<SelectListItem> list = new List<SelectListItem>();
                foreach (Status status in statuss)
                {
                    list.Add(new SelectListItem()
                    {
                        Value = status.StatusID.ToString(),
                        Text = status.Status1
                    });
                }
                return new SelectList(list, "Value", "Text");
            }

        }
        public SelectList GetStates()
        {
            using (var context = new RAI_TestEntities5())
            {
                var states = context.States.ToList();
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
            using (var context = new RAI_TestEntities5())
            {
                var users = context.Users.ToList();
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
            using (var context = new RAI_TestEntities5())
            {
                var clients = context.Clients.ToList();
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


        public List<vw_RAILoans> GetLoans()
        {
            RAI_TestEntities5 context = new RAI_TestEntities5();


            return context.vw_RAILoans.Where(x => x.LoanStatus != "4 - Completed").ToList();
        }

        public Loan GetLoan(int id)
        {
            RAI_TestEntities5 context = new RAI_TestEntities5();
            var loan = context.Loans.FirstOrDefault(l => l.LoanID == id);
            
            return loan;

        }
        public void AddLoan(Loan loan)
        {
            // Get the next available entry ID.
            int nextAvailableEntryId = Data.Loans
                .Max(e => e.LoanID) + 1;

            loan.LoanID = nextAvailableEntryId;

            Data.Loans.Add(loan);
        }
        /// <summary>
        /// Updates an entry.
        /// </summary>
        /// <param name="entry">The entry to update.</param>
        public void UpdateLoan(Loan loan)
        {
            // Find the index of the entry that we need to update.
            int loanIndex = Data.Loans.FindIndex(e => e.LoanID == loan.LoanID);

            if (loanIndex == -1)
            {
                throw new Exception(
                    string.Format("Unable to find an Loan with an ID of {0}", loan.LoanID));
            }

            Data.Loans[loanIndex] = loan;
        }

        /// <summary>
        /// Deletes an entry.
        /// </summary>
        /// <param name="id">The ID of the entry to delete.</param>
        public void DeleteLoan(int id)
        {
            // Find the index of the entry that we need to delete.
            int loanIndex = Data.Loans.FindIndex(e => e.LoanID == id);

            if (loanIndex == -1)
            {
                throw new Exception(
                    string.Format("Unable to find an loan with an ID of {0}", id));
            }

            Data.Loans.RemoveAt(loanIndex);
        }
    }
}