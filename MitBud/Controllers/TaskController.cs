using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MitBud.Providers;
using MitBud.Models;
using System.Text;
using MitBud.DAL;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace MitBud.Controllers
{
    public class TaskController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/SaveTaskLoggedIn")]
        public async Task<HttpResponseMessage> SaveTaskLoggedIn(TaskViewModel taskViewModel)
        {

            var userId = RequestContext.Principal.Identity.GetUserId();

            if (userId != null)
            {
                TaskProvider.SaveTaskLoggedIn(taskViewModel, userId);

                var dd = HttpStatusCode.Accepted;
                var responseMsg = new HttpResponseMessage(dd)
                {
                    Content = new StringContent("", Encoding.UTF8, "application/json")
                };

                sendVerificationByMail(taskViewModel.ClientEmail, taskViewModel.ClientName);

            }
            //else
            //{
            //    AccountController account = new AccountController();

            //    await account.SaveTaskNotLoggedIn(taskViewModel);
            //}
            return Request.CreateResponse(HttpStatusCode.OK);

        }





        [AllowAnonymous]
        [Route("sendVerificationByMail")]
        public string sendVerificationByMail(string currentUser, string name)
        {
            //MailAddress address = new MailAddress(email);
            //string username = address.User;

            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                var mail = new System.Net.Mail.MailMessage();
                mail.From = new MailAddress("mitbud@Outlook.com");
                mail.To.Add(currentUser);
                mail.Subject = "Your Authorization code.";
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Hi " + name + "," + "<br />" + "<br />"
                    + "This is an automatically generated email only to notify you – please do not reply to it." + "<br />" + "<br />"
                    + "Regards, " + "<br />"
                    + "MitBud.";
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential("mitbud@outlook.com", "m42929264.", "Outlook.com");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return "sent";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }



        [System.Web.Http.Route("api/GetTask")]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetTask()
        {
            // var mitBud = new MitBudDBEntities();

            IList<CompanyTaskViewModel> companyTaskVM = null;

            //List<string> ITEMS = new List<string>();

            var CurrentuserId = RequestContext.Principal.Identity.GetUserId();

            using (MitBudDBEntities mitBud = new MitBudDBEntities())
            {
                companyTaskVM = (from task in mitBud.Tasks
                                 from Company_Category in mitBud.Company_Category
                                 where task.Category == Company_Category.Name && Company_Category.CompanyUserId == CurrentuserId
                                 from Company in mitBud.Companies
                                 where Company.PostCode == task.ClientPostCode
                                 select new CompanyTaskViewModel()
                                 {
                                     TaskId = task.TaskId,
                                     Title = task.Title,
                                     Category = task.Category,
                                     ClientPostCode = task.ClientPostCode,
                                     PostCode = Company.PostCode,
                                     ClientName = task.ClientName,
                                     Description = task.Description,


                                 }).Distinct().ToList();



            }
            if (companyTaskVM.Count == 0)
            {
                return NotFound();
            }
            return Ok(companyTaskVM);




        }




    }
}







