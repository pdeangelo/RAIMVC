using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Diagnostics;

namespace RAI_MVC.Repository
{
    public class EntityRepository
    {
        public IList<Entity> GetEntities()
        {
            using (Context context = GetContext())
            {
                return context.Entity
                  .ToList();
            }
        }
        /// <summary>
        /// Returns a single loan.
        /// </summary>
        /// <returns>A fully populated Loan entity instance.</returns>
        public Entity GetEntity(int entityID)
        {
            using (Context context = GetContext())
            {
                return context.Entity
                   .Where(cb => cb.EntityID == entityID)
                   .SingleOrDefault();
            }
        }
        public void AddEntity(Entity entity)
        {
            using (Context context = GetContext())
            {
                context.Entity.Add(entity);

                context.SaveChanges();
            }
        }
        public void UpdateEntity(Entity entity)
        {
            using (Context context = GetContext())
            {
                context.Entity.Attach(entity);

                var loanEntity = context.Entry(entity);

                loanEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteEntity(int entityID)
        {
            using (Context context = GetContext())
            {
                var entity = new Entity() { EntityID = entityID };
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