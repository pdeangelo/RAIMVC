using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RAI_MVC.Models
{
    public class Loan
    {
        // <th>Loan ID</th>
        //<th>Loan Name</th>
        //<th>Funding Date</th>
        //<th>Escrow Date</th>
        //<th>Investor Proceeds Date</th>
        //<th>Status</th>
        //<th>Client</th>
        //<th>Mortgagee</th>
        //<th>Entity</th>
        //<th>Property Address</th>
        //<th>Mortgage Amount</th>
        //<th>Interest Rate</th>
        //<th>Investor Name</th>
        //public int Id { get; set; }
        //[Required]
        //public string LoanNumber { get; set; }
        //[Required]
        //public DateTime FundingDate { get; set; }
        //public DateTime EscrowDate { get; set; }
        //public DateTime InvestorProceedsDate { get; set; }

        //[Display(Name = "Mortgage Amount")]
        //public double LoanMortgageAmount { get; set; }
        //public double LoanInterestRate { get; set; }

        //public bool Appraisal;
        //[Display(Name = "Client")]
        //public int LoanClientID { get; set; }

        //[Display(Name = "Status")]
        //public int LoanStatusID { get; set; }
        //[Display(Name = "Investor")]
        //public int LoanInvestorID { get; set; }
        //public Investor Investor { get; set; }
        //public string LoanMortgagee { get; set; }
        //public string LoanPropertyAddress { get; set; }


        //public Client Client { get; set; }
        //public MiscValue Status { get; set; }
        public Investor Investor { get; set; }
        public Client Client { get; set; }
        public int LoanID { get; set; }
        public int LoanClientID { get; set; }
        public int LoanEntityID { get; set; }
        public int InvestorID { get; set; }
        public int LoanStatusID { get; set; }
        public string LoanNumber { get; set; }
        public DateTime LoanFundingDate { get; set; }
        public string LoanMortgagee { get; set; }
        public string LoanPropertyAddress { get; set; }
        public double LoanInterestRate { get; set; }
        public double LoanMortgageAmount { get; set; }
        public double LoanAdvanceRate { get; set; }
        public int LoanType { get; set; }
        public DateTime LoanEnteredDate { get; set; }
        public DateTime LoanUpdateDate { get; set; }
        public int LoanUpdateUserID { get; set; }
        public string LoanMortgageeBusiness { get; set; }
        public int LoanDwellingType { get; set; }
        public int State { get; set; }

        public Loan()
        {

        }
        public Loan(int loanID, string loanNumber, DateTime loanFundingDate, double loanMortgageAmount, int? loanClientID,
            bool appraisal)
        {
            //Id = loanID;
            //LoanNumber = loanNumber;
            //FundingDate = loanFundingDate;
            //LoanMortgageAmount = loanMortgageAmount;
            //LoanClientID = loanClientID.Value;
            //Appraisal = appraisal;
        }
    }
}