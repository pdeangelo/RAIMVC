using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RAI_MVC.Repository
{
    public class ClientsRepository
    {
        public List<Client> GetClients()
        {
            using (Context context = GetContext())
            {
                return context.Client
                    .OrderBy(c => c.ClientName)
                  .ToList();
            }
        }
        public Client GetClient(int id)
        {
            using (Context context = GetContext())
            {
                return context.Client                 
                   .Where(cb => cb.ClientID == id)
                   .SingleOrDefault();
            }
        }

        public static void AddClient(Client client)
        {
            using (Context context = GetContext())
            {
                context.Client.Add(client);
                                
                context.SaveChanges();
            }
        }
        public void UpdateClient(Client client)
        {
            using (Context context = GetContext())
            {
                context.Client.Attach(client);

                var clientEntry = context.Entry(client);
                clientEntry.State = EntityState.Modified;
                //comicBookEntry.Property("IssueNumber").IsModified = false;

                context.SaveChanges();
            }
        }
        public void DeleteClient(int clientId)
        {
            using (Context context = GetContext())
            {
                var client = new Client() { ClientID = clientId };
                context.Entry(client).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }
    }
}