using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAI_MVC.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public Client (int clientID, string clientName)
        {
            ClientID = clientID;
            ClientName = clientName;
        }
    }
}