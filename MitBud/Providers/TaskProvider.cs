using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitBud.Models;
using MitBud.DAL;
using Microsoft.AspNetCore.Identity;
using MitBud.Controllers;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Routing;
using System.Net;

namespace MitBud.Providers
{
    public class TaskProvider
    {


        //Save Tasks to the database to the Tasks table
        public static void SaveTaskLoggedIn(TaskViewModel TaskViewModel, string userId)
        {
            GenerateRandomPassword();
            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Title = TaskViewModel.Title;
            Task.Client_id = userId;
            Task.Description = TaskViewModel.Description;
            Task.Category = TaskViewModel.Category;
            Task.ClientName = TaskViewModel.ClientName;
            Task.ClientAddress = TaskViewModel.ClientName;
            Task.ClientPostCode = TaskViewModel.ClientPostCode;
            Task.ClientTelephone = TaskViewModel.ClientTelephone;
            Task.ClientEmail = TaskViewModel.ClientEmail;
            Task.WhoAreYou = TaskViewModel.WhoAreYou;
            Task.TaskCost = TaskViewModel.TaskCost;
            db.Tasks.Add(Task);
            db.SaveChanges();


        }

        //Save Tasks to the database to the Tasks table
        //public  static void SaveTask(TaskViewModel TaskViewModel)
        //{
        //    MitBudDBEntities db = new MitBudDBEntities();
        //    AspNetUser asp = new AspNetUser();
        //    Task Task = new Task();
        //    AccountController account = new AccountController();

        //    RegisterClient r = new RegisterClient();
        //    var s = r;
        //    s.Name = TaskViewModel.ClientName;
        //    s.Email = TaskViewModel.ClientEmail;
        //    s.Password = "ATMARr123.";
        //    s.ConfirmPassword = "ATMARr123.";
        //    account.Register_client(r);





        //    Task.Title = TaskViewModel.Title;


        //    Task.Description = TaskViewModel.Description;
        //    Task.Category = TaskViewModel.Category;
        //    Task.ClientName = TaskViewModel.ClientName;
        //    Task.ClientAddress = TaskViewModel.ClientName;
        //    Task.ClientPostCode = TaskViewModel.ClientPostCode;
        //    Task.ClientTelephone = TaskViewModel.ClientTelephone;
        //    Task.ClientEmail = TaskViewModel.ClientEmail;
        //    Task.WhoAreYou = TaskViewModel.WhoAreYou;
        //    Task.TaskCost = TaskViewModel.TaskCost;
        //    db.Tasks.Add(Task);
        //    db.SaveChanges();


        //}


        public static void SaveTask(TaskViewModel TaskViewModel, string userId)
        {


            MitBudDBEntities db = new MitBudDBEntities();
            Task Task = new Task();
            Task.Client_id = userId;
            Task.Title = TaskViewModel.Title;
            Task.Description = TaskViewModel.Description;
            Task.Category = TaskViewModel.Category;
            Task.ClientName = TaskViewModel.ClientName;
            Task.ClientAddress = TaskViewModel.ClientName;
            Task.ClientPostCode = TaskViewModel.ClientPostCode;
            Task.ClientTelephone = TaskViewModel.ClientTelephone;
            Task.ClientEmail = TaskViewModel.ClientEmail;
            Task.WhoAreYou = TaskViewModel.WhoAreYou;
            Task.TaskCost = TaskViewModel.TaskCost;
            db.Tasks.Add(Task);
            db.SaveChanges();

            //SendPasswordResetEmail(TaskViewModel.ClientEmail, TaskViewModel.ClientName);


            //ChangePasswordBindingModel ch = new ChangePasswordBindingModel();
            //ch.OldPassword = randomPass;



        }



        //public static void SendPasswordResetEmail(string ToEmail, string UserName)
        //{
        //    // MailMessage class is present is System.Net.Mail namespace
        //    MailMessage mailMessage = new MailMessage("atmar@hotmail.dk", ToEmail);


        //    // StringBuilder class is present in System.Text namespace
        //    StringBuilder sbEmailBody = new StringBuilder();
        //    sbEmailBody.Append("Dear " + UserName + ",<br/><br/>");
        //    sbEmailBody.Append("Please click on the following link to reset your password");
        //    sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost/WebApplication1/Registration/ChangePassword.aspx?uid=");
        //    sbEmailBody.Append("<br/><br/>");
        //    sbEmailBody.Append("<b>Pragim Technologies</b>");

        //    mailMessage.IsBodyHtml = true;

        //    mailMessage.Body = sbEmailBody.ToString();
        //    mailMessage.Subject = "Reset Your Password";
        //    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

        //    smtpClient.Credentials = new System.Net.NetworkCredential()
        //    {
        //        UserName = "atmar@hotmail.dk",
        //        Password = "mursal1506"
        //    };

        //    smtpClient.EnableSsl = true;
        //    smtpClient.Send(mailMessage);
        //}














        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

    }
}