using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAI_MVC.Models
{
    public class LoanType
    {
        public LoanType()
        {
            Loans = new HashSet<Loan>();
        }

        public int LoanTypeID { get; set; }
        public string LoanTypeName { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
