using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rental.ViewModels;
using Rental.Utils;
using System.IO;
using Rental.Models;

namespace Rental.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //send email via contact page
        public ActionResult Contact()
        {
            return View(new SendEmailViewModel());
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Contact(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    var attachment = Request.Files["attachment"];

                    //check upload
                    if (attachment.ContentLength > 0)
                    {
                        String path = Path.Combine(Server.MapPath("~/Content/Attachment/"), attachment.FileName);
                        attachment.SaveAs(path);

                        EmailSender es = new EmailSender();
                        es.SendAttachment(toEmail, subject, contents, path, attachment.FileName);
                    }
                    else
                    {
                        EmailSender es = new EmailSender();
                        es.Send(toEmail, subject, contents);
                    }
                    // save file to server


                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }

                catch
                {
                    return View();
                }
            }

            return View();
        }

            //public ActionResult About()
            //{
            //    ViewBag.Message = "Your application description page.";

            //    return View();
            //}


           public ActionResult About()
            {
            return View(new BulkEmailViewModel());
            }

        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult About(BulkEmailViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    String subject = model.Subject;
                    String contents = model.Contents;


                    BulkEmailSender es = new BulkEmailSender();
                    es.BulkSend(subject,contents);

                    ViewBag.Result = "Email has been sent";

                    ModelState.Clear();

                    return View(new BulkEmailViewModel());
                }
                catch
                {
                    ViewBag.Result = "Failed!";
                    return View();
                }
            }

            return View();
        }




    }
}