using Microsoft.AspNet.Identity.EntityFramework;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Rental.Utils
{
    public class BulkEmailSender
    {
        // sendgrid API KEY here.
        private const String API_KEY = "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";

        public void BulkSend(String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@waverley.bike", subject);
            var to = new List<EmailAddress>();
            var plainTextContent = contents;
            //var htmlContent = "<p>" + contents + "</p>";
            var htmlContent = contents;
            //Get list of Emails from aspnetusers table 
            var emailList = new IdentityDbContext();
            var users = emailList.Users.ToList();
            //Populate to list
            for (int i = 0; i < users.Count; i++)
            {
                var CustomerEmail = users[i].Email;
                var CustomerName = users[i].UserName;
                to.Add(new EmailAddress(CustomerEmail, CustomerName));
            }

            //Create message
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }
    }
}