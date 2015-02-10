using FInalProject.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace FInalProject.Models
{
    public class EmailManger
    {
        private  readonly string From = "cchelpdeskemail@gmail.com";
        public  string To { get; set; }
        public  string Subject { get; set; }

        public  string Body { get; set; }

        private  SmtpClient _client;
        private  readonly string UserName = "cchelpdeskemail@gmail.com";
        private  readonly string Password = "CC2015NET";
        private  readonly int Port = 587;
        private  readonly string Host = "smtp.gmail.com";
        public EmailManger()
        {
            _client = new SmtpClient(Host, Port);
            _client.UseDefaultCredentials = false;
            _client.Credentials = new System.Net.NetworkCredential(UserName, Password);
            _client.EnableSsl = true;
            
        }

        public bool SendEmail(QuizSurveyShareViewModel model)
        { 
            this.Subject = "A quiz shared with you!";
            this.Body = "Hey! Please clik this link to take the quiz : ";
            this.Body += model.Link;
            this.To = model.EmailToShare;
            
            MailMessage mail = new MailMessage(From, To, Subject, Body);

            try
            {
               
                _client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}