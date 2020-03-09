using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitBud.DAL;
using MitBud.Models;

namespace MitBud.Providers
{
    public class ClientProvider
    {
        public static void SaveClientInfo(string name, string email, string UserId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Client client = new Client();


            client.Client_Id = UserId;
            client.Name = name;
            client.Email = email;
            //client.Name = registerViewModel.Name;
            //client.Email = registerViewModel.Email;

            db.Clients.Add(client);

            db.SaveChanges();


        }

        //public static void SaveClientId(string UserId)
        //{
        //    MitBudDBEntities db = new MitBudDBEntities();
        //    Task task = new Task();
        //    task.Client_id = UserId;
        //    db.Tasks.Add(task);
        //    db.SaveChanges();
        //}
    }
}