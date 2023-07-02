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
        public Loan()
        {

        }

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
        //Computed Fields
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double DaysOutstandingClosed
        {
            get {
                if (InvestorProceedsDate == null)
                    return 0;
                else
                    return (double)DateDepositedInEscrow.Value.Subtract(InvestorProceedsDate.Value).TotalDays;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double DaysOutstandingPending
        {
            get
            {
                if (InvestorProceedsDate == null)
                    return 0;
                else if (OpenMortgageBalance > 1)
                    return (double)DateTime.Now.Subtract(InvestorProceedsDate.Value).TotalDays;
                else
                    return 0;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? LoanAdvanceAmount
        {
            get { return LoanMortgageAmount * LoanAdvanceRate; }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? LoanReserveAmount
        {
            get { return LoanMortgageAmount * (1 - LoanAdvanceRate); }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? OpenMortgageBalance
        {
            get
            {
                if (InvestorProceeds == 0 && this.LoanStatus.LoanStatusName != "1 - Underwriting")
                    return LoanMortgageAmount;
                else
                    return 0;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? OpenAdvanceBalance
        {
            get
            {
                if (InvestorProceeds == 0 && this.LoanStatus.LoanStatusName != "1 - Underwriting")
                    return LoanAdvanceAmount;
                else
                    return 0;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? MortgageOriginatorProceeds
        {
            get
            {
                return InvestorProceeds - TotalFees - LoanAdvanceAmount;
            }
        }
        //[fn_RAIOriginationDiscount]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? OriginationDiscountFee
        {
            get
            {
                double originationDiscount = 0;
                double daysOutstandingCalc = 0;

                if (DaysOutstandingClosed > 0)
                {
                    daysOutstandingCalc = DaysOutstandingClosed;
                    //Period 1

                    daysOutstandingCalc = daysOutstandingCalc - OriginationDiscountNumDays.Value;

                    //Period 2

                    if (daysOutstandingCalc > 0)
                    {
                        daysOutstandingCalc = daysOutstandingCalc - OriginationDiscountNumDays2.Value;
                        originationDiscount += OriginationDiscount2.Value;
                    }
                    // Period 3

                    if (daysOutstandingCalc > 0)
                    {
                        daysOutstandingCalc = daysOutstandingCalc - OriginationDiscountNumDays2.Value;
                        originationDiscount += OriginationDiscount2.Value;
                    }
                }

                else
                {
                    originationDiscount = 0;
                }
                    return originationDiscount;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? UnderwritingFeeCalc
        {
            get
            {
                if (DaysOutstandingClosed > 0)
                    return 100.0;
                else
                    return 0.0;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? LoanMinimumInterest
        {
            get {
                double minInterest = 0;
                double UseMinRate = 0;
                if (this.Client.MinimumInterest > 0)
                    UseMinRate = this.Client.MinimumInterest;

                else if (this.Client.ClientPrimeRateSpread > 0)
                    UseMinRate = this.Client.ClientPrimeRateSpread + this.Client.ClientPrimeRate;

                else
                    UseMinRate = LoanInterestRate.Value;

                if (UseMinRate < LoanInterestRate)
                    MinimumInterest = LoanInterestRate;

                else
                    MinimumInterest = UseMinRate;

                return minInterest;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? DailyInterestRate
        {
            get
            {
                return MinimumInterest / 360;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? InterestIncome
        {
            get
            { double interestIncome = 0;
                if (InterestBasedOnAdvance.Value)
                    interestIncome = DaysOutstandingClosed * DailyInterestRate.Value * LoanAdvanceAmount.Value;
                else
                    interestIncome = DaysOutstandingClosed * DailyInterestRate.Value * LoanMortgageAmount.Value;
                return interestIncome;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double AnnualizedYield
        {
            get
            {
                if (InvestorProceeds == null || LoanAdvanceAmount == null || DaysOutstandingClosed == null)
                    return 0;
                if (InvestorProceeds.Value > 0 && LoanAdvanceAmount.Value != 0 && DaysOutstandingClosed > 0)
                    return (double)(TotalFees / LoanAdvanceAmount / DaysOutstandingClosed * 360);
                else
                    return 0;
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? TotalTransfer
        {
            get
            {
                return InvestorProceeds -  LoanAdvanceAmount - (TotalFees);
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double? TotalFees
        {
            get
            {
                return(InterestFee +  OriginationFee + UnderwritingFee +
                       CustSvcInterestDiscount +  CustSvcOriginationDiscount + CustSvcUnderwritingDiscount);
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double WireFee
        {
            get
            {
                return (25);
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double AdvanceWithWireFee
        {
            get
            {
                return LoanAdvanceAmount.Value - WireFee;
                
            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double GreaterofMinandCouponInterest
        {
            get
            {
                return MinimumInterest.Value > LoanInterestRate.Value ? MinimumInterest.Value : LoanInterestRate.Value;

            }
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Double AppraisalPcnt
        {
            get
            {
                return LoanUWAppraisalAmount == null || LoanUWAppraisalAmount.Value == 0  ? 0 : LoanMortgageAmount.Value / LoanUWAppraisalAmount.Value;

            }
        }

        //End Computed Fields
        public virtual LoanStatus LoanStatus { get; set; }
        public virtual LoanType LoanType { get; set; }
        public virtual Client Client { get; set; }
        public virtual DwellingType DwellingType { get; set; }
        public virtual State State { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual Investor Investor { get; set; }

    }
}
