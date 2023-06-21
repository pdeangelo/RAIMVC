using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAIModel.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string WinUserID { get; set; }

        public int RoleID { get; set; }

        public int OfficeID { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public int Status { get; set; }

        public int? FinanceGroup { get; set; }

        public bool? AllSecurityAccess { get; set; }

        public bool? AllPipelineAccess { get; set; }

        public bool? IsAnalyst { get; set; }

        public bool? IsFinance { get; set; }

        public bool? IsManager { get; set; }

        public bool? FundLevelAccess { get; set; }

        public int? Client { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        public virtual Role Role { get; set; }
    }
}
