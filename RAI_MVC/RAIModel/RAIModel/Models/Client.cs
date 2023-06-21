using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAIModel.Models
{
    public class Client
    {
        public Client()
        {
            Loans = new List<Loan>();
        }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
