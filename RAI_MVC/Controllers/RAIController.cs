using RAI_MVC.Models;
using RAI_MVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RAI_MVC.Controllers
{
    public class RAIController : Controller
    {
        private UsersRepository _usersRepository = null;
        private LoanRepository _loanRepository = null;
        public RAIController()
        {
            _usersRepository = new UsersRepository();
            _loanRepository = new LoanRepository();
        }
        public ActionResult Detail(int? id)
        {
            //if (userID == null)
            //{
            //    return HttpNotFound();
            //}
            var user = _loanRepository.GetLoan(id.Value);
            
            return View(user);
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Loan loan = _loanRepository.GetLoans
        //    if (loan == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(loan);
        //}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //_loanRepository.DeleteLoan(id);

            //TempData["Message"] = "Loan Successfully Deleted";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Loan loan = _loanRepository.GetLoan((int)id);
            if (loan == null)
            {
                return HttpNotFound();
            }


            SetupSelectListItems();
            return View(loan);
        }
        [HttpPost]
        public ActionResult Edit(Loan loan)
        {
            //ValidateLoan(loan);
            //if (ModelState.IsValid)
            //{
            //    _loanRepository.UpdateLoan(loan);

            //    TempData["Message"] = "Loan Successfully Updated";
            //    return RedirectToAction("Index");
            //}

            SetupSelectListItems();
            return View(loan);
        }

        public ActionResult Index(string SearchText, string StatusSelectListItems, string EntitiesSelectListItems, string ClientsSelectListItems, string chkShowCompleted, string submitButton)
        {
            //List<vw_RAILoans> raiLoans = _loanRepository.GetLoans(StatusSelectListItems, EntitiesSelectListItems, ClientsSelectListItems, chkShowCompleted, SearchText);
            var raiLoans = _loanRepository.GetLoans();

            SetupSelectListItems();
            return View(raiLoans);
        }
        [HttpPost]
        public ActionResult SubmitSelected(List<Loan> x, FormCollection collection)
        {

            return RedirectToAction("Index");
        }
        public ActionResult Add()
        {
            var loan = new Loan();

            SetupSelectListItems();
            return View(loan);
        }
        [HttpPost]
        public ActionResult Add(Loan loan)
        {
            ValidateLoan(loan);
            if (ModelState.IsValid)
            {
                //_loanRepository.AddLoan(loan);
                //TempData["Message"] = "Loan Successfully Added";
                return Redirect("Index");
            }
            SetupSelectListItems();
            return View(loan);
        }
        public void ValidateLoan(Loan loan)
        {
            if (ModelState.IsValidField("LoanMortgageAmount") && loan.LoanMortgageAmount <= 0)
            {
                ModelState.AddModelError("LoanMortgageAmount", "The mortgage amount must be greater than 0");
            }
            
        }
        private void SetupClientsSelectListItems()
        {
            ViewBag.ClientsSelectListItems = _loanRepository.GetClients();

        }
        private void SetupEntitiesSelectListItems()
        {
            ViewBag.EntitiesSelectListItems = _loanRepository.GetEntities();

        }
        private void SetupStatesSelectListItems()
        {
            ViewBag.StatesSelectListItems = _loanRepository.GetStates();

        }
        private void SetupLoanTypesSelectListItems()
        {
            ViewBag.LoanTypeSelectListItems = _loanRepository.GetTypes();

        }
        private void SetupInvestorsSelectListItems()
        {
            ViewBag.InvestorsSelectListItems = _loanRepository.GetInvestors();

        }
        private void SetupUsersSelectListItems()
        {
            ViewBag.UsersSelectListItems = _loanRepository.GetUsers();

        }
        private void SetupStatusSelectListItems()
        {
            ViewBag.StatusSelectListItems = _loanRepository.GetStatus();

        }
        private void SetupSelectListItems()
        {
            SetupClientsSelectListItems();
            SetupEntitiesSelectListItems();
            SetupStatesSelectListItems();
            SetupLoanTypesSelectListItems();
            SetupInvestorsSelectListItems();
            SetupUsersSelectListItems();
            SetupStatusSelectListItems();
        }
    }
}