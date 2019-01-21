using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Email
    {
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = @"davidhoro20@gmail.com";
        private string Pasword = "lvkretuzieqoxlic";
        private string From = @"davidhoro20@gmail.com";

        public string Recipients { get; set; }
        public string Subject { get; set; }
        public Email(string Recipients, string Subject)
        {
            this.Recipients = Recipients;
            this.Subject = Subject;
        }

        public bool SendingEmail(string body)
        {
            try
            {
                var client = new SmtpClient(this.Host, this.Port)
                {
                    Credentials = new NetworkCredential(this.User, this.Pasword),
                    EnableSsl = true
                };
                MailMessage mailMessage = new MailMessage(this.From, this.Recipients) { Body = body };
                client.Send(mailMessage);
                //client.Send(Configuration.SenderEmailAddress, recipients, subject, body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
