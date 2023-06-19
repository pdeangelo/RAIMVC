using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Repository
{
    public class UsersRepository
    {
        private static User[] _users = new User[]
        {
            new User()
            {
                UserID=1,
                UserName="Paul",
                WinUserID="paul",
                Password="password",
                //IsADmin = true,
                //roles = new string[4] {"Volvo", "BMW", "Ford", "Mazda"}
    }
        };

        public User GetUser(int userID)
        {
            User user = null;

            foreach (var u in _users)
            {
                if (u.UserID == userID)
                {
                    user = u;
                    break;
                }
            }
            return user;

        }
        public User[] GetUsers()
        {
            
            return _users;

        }
    }
}