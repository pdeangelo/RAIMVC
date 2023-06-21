using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAIModel.Models
{
    public class Investor
    {
        public int InvestorID { get; set; }

        [StringLength(200)]
        public string InvestorName { get; set; }

        [StringLength(200)]
        public string CustodianName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
