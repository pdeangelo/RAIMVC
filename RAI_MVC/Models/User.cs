using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAI_MVC.Models
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

        public bool IsAdmin { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        public virtual Role Role { get; set; }

    }
}
