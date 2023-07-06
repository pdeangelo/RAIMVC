using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RAI_MVC.Repository
{
    public class UsersRepository
    {
        public IList<User> GetUsers()
        {
            using (Context context = GetContext())
            {
                return context.User
                  .Include(l => l.Role)
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public User GetUser(int userID)
        {
            using (Context context = GetContext())
            {
                return context.User
                   .Where(cb => cb.UserID == userID)
                   .SingleOrDefault();
            }
        }
        public void AddUser(User user)
        {
            using (Context context = GetContext())
            {
                context.User.Add(user);

                context.SaveChanges();
            }
        }
        public void UpdateUser(User user)
        {
            using (Context context = GetContext())
            {
                context.User.Attach(user);

                var loanEntity = context.Entry(user);

                loanEntity.State = EntityState.Modified;
                context.SaveChanges();

            }
        }
        public void DeleteUser(int userID)
        {
            using (Context context = GetContext())
            {
                var entity = new User() { UserID = userID };
                context.Entry(entity).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }



        public IList<Role> GetRoles()
        {
            using (Context context = GetContext())
            {
                return context.Role
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public Role GetRole(int roleID)
        {
            using (Context context = GetContext())
            {
                return context.Role
                   .Where(cb => cb.RoleID == roleID)
                   .SingleOrDefault();
            }
        }
        public void AddRole(Role role)
        {
            using (Context context = GetContext())
            {
                context.Role.Add(role);

                context.SaveChanges();
            }
        }
        public void UpdateRole(Role role)
        {
            using (Context context = GetContext())
            {
                context.Role.Attach(role);

                var loanEntity = context.Entry(role);

                loanEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteRole(int roleID)
        {
            using (Context context = GetContext())
            {
                var entity = new Role() { RoleID = roleID };
                context.Entry(entity).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Private method that returns a database context.
        /// </summary>
        /// <returns>An instance of the Context class.</returns>
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

    }
}