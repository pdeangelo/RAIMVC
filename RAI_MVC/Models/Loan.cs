using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAI_MVC.Models
{
    public class Loan
    {
        public int LoanID { get; set; }
        public int? LoanStatusID { get; set; }
        public int? ClientID { get; set; }
        public int? DwellingTypeID { get; set; }
        public int? StateID { get; set; }
        public int? EntityID { get; set; }
        public int? InvestorID { get; set; }

        public int? LoanTypeID { get; set; }

        public string LoanNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LoanFundingDate { get; set; }

        [StringLength(200)]
        public string LoanMortgagee { get; set; }

        public string LoanPropertyAddress { get; set; }

        public double? LoanInterestRate { get; set; }

        public double? LoanMortgageAmount { get; set; }

        public double? LoanAdvanceRate { get; set; }


        [Column(TypeName = "date")]
        public DateTime? LoanEnteredDate { get; set; }

        public DateTime? LoanUpdateDate { get; set; }

        public int? LoanUpdateUserID { get; set; }

        [StringLength(200)]
        public string LoanMortgageeBusiness { get; set; }

        //UW Fields
        public bool? LoanUW10031008LoanApplication { get; set; }

        public bool? LoanUWAllongePromissoryNote { get; set; }

        public bool? LoanUWAppraisal { get; set; }

        public double? LoanUWAppraisalAmount { get; set; }

        public double? LoanUWPostRepairAppraisalAmount { get; set; }

        public bool? LoanUWAssignmentOfMortgage { get; set; }

        public bool? LoanUWBackgroundCheck { get; set; }

        public bool? LoanUWCertofGoodStandingFormation { get; set; }

        public bool? LoanUWClosingProtectionLetter { get; set; }

        public bool? LoanUWCommitteeReview { get; set; }

        public bool? LoanUWCreditReport { get; set; }

        public bool? LoanUWFloodCertificate { get; set; }

        public bool? LoanUWHUD1SettlementStatement { get; set; }

        public bool? LoanUWHomeownersInsurance { get; set; }

        public bool? LoanUWLoanPackage { get; set; }

        public bool? LoanUWLoanSizerLoanSubmissionTape { get; set; }

        public bool? LoanUWTitleCommitment { get; set; }

        public bool? LoanUWClaytonReportApprovalEmail { get; set; }

        public double? LoanUWZillowSqFt { get; set; }

        public bool? LoanUWIsComplete { get; set; }

        public int? CompletedBy { get; set; }

        //end UW Fields
        //Fudning Fields 
        [Column(TypeName = "date")]
        public DateTime? DateDepositedInEscrow { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BaileeLetterDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InvestorProceedsDate { get; set; }

        public double? InvestorProceeds { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ClosingDate { get; set; }
        //End funding fields
        //Fee Fields
        public double? MinimumInterest { get; set; }

        public double? OriginationDiscount { get; set; }

        public double? OriginationDiscount2 { get; set; }

        public int? OriginationDiscountNumDays { get; set; }

        public int? OriginationDiscountNumDays2 { get; set; }

        public bool? InterestBasedOnAdvance { get; set; }

        public bool? OriginationBasedOnAdvance { get; set; }

        public bool? NoInterest { get; set; }

        public double? InterestFee { get; set; }

        public double? OriginationFee { get; set; }

        public double? UnderwritingFee { get; set; }

        public double? CustSvcUnderwritingDiscount { get; set; }

        public double? CustSvcInterestDiscount { get; set; }

        public double? CustSvcOriginationDiscount { get; set; }

        public double? ClientPrimeRate { get; set; }

        public double? ClientPrimeRateSpread { get; set; }

        //end fee fields

        public LoanStatus LoanStatus { get; set; }
        public LoanType LoanType { get; set; }
        public Client Client { get; set; }
        public DwellingType DwellingType { get; set; }
        public State State { get; set; }
        public Entity Entity { get; set; }
        public Investor Investor { get; set; }

    }
}
