using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BlowOutNew.Models;

namespace BlowOutNew.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Email (string name, string email)
        {
            ViewBag.Name = name;
            ViewBag.Email = email;
            
            return View("Email");
        }

        public ActionResult AboutUs()
        {
            return View();
        }


        //Possible code for sending smtp emails

        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        public void SendEmail(string toAddress, string fromAddress,
                      string subject, string message)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    const string email = "ashlynlewis1@gmail.com";
                    const string password = "trashtan143";

                    var loginInfo = new System.Net.NetworkCredential(email, password);


                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(new MailAddress(toAddress));
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;

                    try
                    {
                        using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                        {
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = loginInfo;
                            smtpClient.Send(mail);
                        }

                    }

                    finally
                    {
                        //dispose the client
                        mail.Dispose();
                    }

                }
            }
            catch (SmtpFailedRecipientsException ex)
            {
                foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
                {
                    var status = t.StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Response.Write("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        //resend
                        //smtpClient.Send(message);
                    }
                    else
                    {
                        Response.Write("Failed to deliver message to {0}"
                                          /*t.FailedRecipient*/);
                    }
                }
            }
            catch (SmtpException Se)
            {
                // handle exception here
                Response.Write(Se.ToString());
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

        }

        [HttpPost]
        public ActionResult Contact(ContactRequest contact)
        {
            if (ModelState.IsValid)
            {
                //prepare email
                var toAddress = "ashlynlewis1@gmail.com";
                var fromAddress = contact.emailAddress.ToString();
                var subject = "Test inquiry from " + contact.subject;
                var message = new StringBuilder();
                message.Append("Name: " + contact.firstName + contact.lastName + "\n");
                message.Append("Email: " + contact.emailAddress + "\n");
                message.Append("phone: " + contact.phoneNumber + "\n");
                message.Append("Subject: " + contact.subject + "\n\n");
                message.Append(contact.Message);

                //start email Thread
                var tEmail = new Thread(() =>
               SendEmail(toAddress, fromAddress, subject, contact.Message));
                tEmail.Start();
            }
            return View("Email", contact);
        }
    }
}