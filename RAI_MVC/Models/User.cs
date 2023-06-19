using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Models
{
    public class User
    {
        public int UserID { get; set; }        
        public string UserName { get; set; }        
        public string WinUserID { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }       
        public int Client { get; set; }        
        public string RoleName { get; set; }       
        public int OfficeID { get; set; }        
        public string Email { get; set; }       
        public bool IsADmin { get; set; }   
        public string[] roles { get; set; }
    }
}