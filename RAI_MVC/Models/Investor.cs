using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Models
{
    public class Investor
    {
        public int InvestorID { get; set; }
        public string InvestorName { get; set; }
        public string CustodianName { get; set; }
        public Investor()
        {
        }
        public Investor(int investorID, string investorName)
        {
        InvestorID = investorID;
        InvestorName = investorName;
        }
    }
}