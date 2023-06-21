using RAIModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RAIModel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {

                var loans = context.Loans
                  .Include(l => l.State)
                  .Include(l => l.Client)
                  .Include(l => l.LoanStatus)
                  .Include(l => l.Entity)
                  .Include(l => l.Investor)
                  .Include(l => l.DwellingType)
                  .Include(l => l.LoanType)
                  .ToList();
            }


        }
    }
}
