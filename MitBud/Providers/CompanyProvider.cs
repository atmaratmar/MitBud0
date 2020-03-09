using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitBud.Models;
using MitBud.DAL;

namespace MitBud.Providers
{
    public class CompanyProvider
    {
        public static void SaveCompanyInfo(RegisterCompany registerViewModel, string UserId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Company company = new Company();
            company.UserId = UserId;
            company.CompanyName = registerViewModel.CompanyName;
            company.Telephone = registerViewModel.Telephone;
            company.CompanySize = registerViewModel.CompanySize;
            company.City = registerViewModel.City;
            company.CVR = registerViewModel.CVR;
            company.Email = registerViewModel.Email;
            company.ContactPerson = registerViewModel.ContactPerson;
            company.Address = registerViewModel.Address;
            company.PostCode = registerViewModel.PostCode;
            db.Companies.Add(company);
            db.SaveChanges();


        }

        public static void SaveCategory(RegisterCompany registerViewModel, string UserId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Company_Category company_category = new Company_Category();
            company_category.CompanyUserId = UserId;
            company_category.Name = registerViewModel.Category;

            db.Company_Category.Add(company_category);
            db.SaveChanges();


        }

        public static void SaveConversation(ConversationViewModel conversation, string UserId, string clientId)
        {
            MitBudDBEntities db = new MitBudDBEntities();
            Task task = new Task();
            Conversation con = new Conversation();
            con.Company_Id = UserId;
            con.Task_Id = conversation.TaskID;
            con.Message = conversation.Message;
            con.Client_Id = clientId;
            db.Conversations.Add(con);
            db.SaveChanges();


        }






    }
}