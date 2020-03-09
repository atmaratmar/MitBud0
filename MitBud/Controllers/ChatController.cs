using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MitBud.Models;
using MitBud.Providers;
using MitBud.DAL;
using System.Net.Mail;

namespace MitBud.Controllers
{
    public class ChatController : ApiController
    {


        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/saveConversation")]
        public async Task<HttpResponseMessage> SaveConversation(ConversationViewModel conversation)
        {
            MitBudDBEntities mitBudDB = new MitBudDBEntities();

            var userId = RequestContext.Principal.Identity.GetUserId();

            var CompanyEmail = mitBudDB.Companies.Where(x => x.UserId == conversation.Company_Id).SingleOrDefault();

            var clientEmail = mitBudDB.Clients.Where(x => x.Client_Id == conversation.Client_id).SingleOrDefault();

            var clientId = mitBudDB.Tasks.Where(x => x.TaskId == conversation.TaskID).SingleOrDefault();

            //var companyName = CompanyEmail.CompanyName;

            CompanyProvider.SaveConversation(conversation, userId, clientId.Client_id);

            var statusCode = HttpStatusCode.Accepted;

            var responseMsg = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };


            // sendNotificationEmail(clientEmail.Name, CompanyEmail.CompanyName);
            return responseMsg;
        }




        //[AllowAnonymous]
        //[Route("sendVerificationByMail")]
        //public string sendNotificationEmail(string clientName, string companyName)
        //{
        //    //MailAddress address = new MailAddress(email);
        //    //string username = address.User;

        //    try
        //    {

        //        SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
        //        var mail = new System.Net.Mail.MailMessage();
        //        mail.From = new MailAddress("mitbud@outlook.com");
        //        mail.To.Add(clientName);
        //        mail.Subject = "Your Authorization code.";
        //        mail.IsBodyHtml = true;
        //        string htmlBody;
        //        htmlBody = "Hi " + clientName + "," + "<br />" + "<br />"
        //            + "You have received an offer from" + companyName  + "<br />" + "<br />"
        //            + "Regards, " + "<br />"
        //            + "MicroLendr.";
        //        mail.Body = htmlBody;
        //        SmtpServer.Port = 587;
        //        SmtpServer.UseDefaultCredentials = false;
        //        SmtpServer.Credentials = new NetworkCredential("mitbud@outlook.com", "m42929264.", "outlook.cm");
        //        SmtpServer.EnableSsl = true;
        //        SmtpServer.Send(mail);

        //        return "sent";
        //    }
        //    catch (Exception ex)
        //    {

        //        return ex.Message;
        //    }



        [System.Web.Http.HttpGet]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("api/getMessage")]
        public IHttpActionResult GetMessage()
        {

            IList<ConversationViewModel> conversation = null;
            var CurrentuserId = RequestContext.Principal.Identity.GetUserId();

            using (MitBudDBEntities mitBud = new MitBudDBEntities())
            {
                //Table name (Comments)
                conversation = (from conv in mitBud.Conversations
                                where conv.Client_Id == CurrentuserId
                                select new ConversationViewModel()
                                {

                                    Company_Id = conv.Company_Id,
                                    Client_id = conv.Client_Id,
                                    Message = conv.Message


                                }).ToList();
            }

            if (conversation.Count == 0)
            {
                return NotFound();
            }
            return Ok(conversation);
        }






    }
}
