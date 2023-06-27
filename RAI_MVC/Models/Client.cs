using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAI_MVC.Models
{
    public class Client
    {
        public Client()
        {
            Loans = new List<Loan>();
        }
        public int ClientID { get; set; }
        public string ClientName { get; set; }

        public double AdvanceRate { get; set; }
        public double MinimumInterest { get; set; }
        public double ClientPrimeRate { get; set; }
        public double ClientPrimeRateSpread { get; set; }
        public double OriginationDiscount { get; set; }
        public double OriginationDiscount2 { get; set; }
        public int OriginationDiscountNumDays { get; set; }
        public int OriginationDiscountNumDays2 { get; set; }
        public bool InterestBasedOnAdvance { get; set; }
        public bool OriginationBasedOnAdvance { get; set; }
        public bool NoInterest { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
