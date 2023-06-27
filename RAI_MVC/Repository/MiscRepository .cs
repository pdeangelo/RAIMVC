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
    public class MiscRepository
    {
        public IList<LoanStatus> GetLoanStatuss()
        {
            using (Context context = GetContext())
            {
                return context.LoanStatus
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public LoanStatus GetLoanStatus(int loanStatusID)
        {
            using (Context context = GetContext())
            {
                return context.LoanStatus
                   .Where(cb => cb.LoanStatusID == loanStatusID)
                   .SingleOrDefault();
            }
        }
        public static void AddLoanStatus(LoanStatus loanStatus)
        {
            using (Context context = GetContext())
            {
                context.LoanStatus.Add(loanStatus);

                context.SaveChanges();
            }
        }
        public void UpdateLoanStatus(LoanStatus loanStatus)
        {
            using (Context context = GetContext())
            {
                context.LoanStatus.Attach(loanStatus);

                var loanEntry = context.Entry(loanStatus);
               
                context.SaveChanges();
            }
        }
        public void DeleteLoanStatus(int loanStatusID)
        {
            using (Context context = GetContext())
            {
                var loanStatus = new LoanStatus() { LoanStatusID = loanStatusID };
                context.Entry(loanStatus).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        //
        public IList<DwellingType> GetDwellingTypes()
        {
            using (Context context = GetContext())
            {
                return context.DwellingType
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public DwellingType GetDwellingType (int dwellingTypeID)
        {
            using (Context context = GetContext())
            {
                return context.DwellingType
                   .Where(cb => cb.DwellingTypeID == dwellingTypeID)
                   .SingleOrDefault();
            }
        }
        public static void AddDwellingType(DwellingType dwellingType)
        {
            using (Context context = GetContext())
            {
                context.DwellingType.Add(dwellingType);

                context.SaveChanges();
            }
        }
        public void UpdateDwellingType(DwellingType dwellingType)
        {
            using (Context context = GetContext())
            {
                context.DwellingType.Attach(dwellingType);

                var loanEntry = context.Entry(dwellingType);

                context.SaveChanges();
            }
        }
        public void DeleteDwellingType(int dwellingTypeID)
        {
            using (Context context = GetContext())
            {
                var dwellingType = new DwellingType() { DwellingTypeID = dwellingTypeID };
                context.Entry(dwellingType).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        //
        public IList<State> GetStates()
        {
            using (Context context = GetContext())
            {
                return context.State
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public State GetState(int stateID)
        {
            using (Context context = GetContext())
            {
                return context.State
                   .Where(cb => cb.StateID == stateID)
                   .SingleOrDefault();
            }
        }
        public static void AddState(State state)
        {
            using (Context context = GetContext())
            {
                context.State.Add(state);

                context.SaveChanges();
            }
        }
        public void UpdateState(State state)
        {
            using (Context context = GetContext())
            {
                context.State.Attach(state);

                var loanEntry = context.Entry(state);

                context.SaveChanges();
            }
        }
        public void DeleteState(int stateID)
        {
            using (Context context = GetContext())
            {
                var state = new State() { StateID = stateID };
                context.Entry(state).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        //
        public IList<LoanType> GetLoanTypes()
        {
            using (Context context = GetContext())
            {
                return context.LoanType
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public LoanType GetLoanType(int loanTypeID)
        {
            using (Context context = GetContext())
            {
                return context.LoanType
                   .Where(cb => cb.LoanTypeID == loanTypeID)
                   .SingleOrDefault();
            }
        }
        public static void AddLoanType(LoanType loanType)
        {
            using (Context context = GetContext())
            {
                context.LoanType.Add(loanType);

                context.SaveChanges();
            }
        }
        public void UpdateLoanType(LoanType loanType)
        {
            using (Context context = GetContext())
            {
                context.LoanType.Attach(loanType);

                var loanEntry = context.Entry(loanType);

                context.SaveChanges();
            }
        }
        public void DeleteLoanType(int loanTypeID)
        {
            using (Context context = GetContext())
            {
                var loanType = new LoanType() { LoanTypeID = loanTypeID };
                context.Entry(loanType).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        //
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