using RAI_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Repository
{
    public class ClientsRepository
    {
        public List<Client> GetClients()
        {
            return Data.Clients
                .OrderBy(a => a.ClientName)
                .ToList();
        }
    }
}