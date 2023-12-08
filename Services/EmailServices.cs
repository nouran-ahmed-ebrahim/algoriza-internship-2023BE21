using Castle.Core.Smtp;
using Core.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailServices : IEmailServices
    {
        public Task SendEmailAsync(string receiverEmail, string subject, string message)
        {
            var SenderEmail = "nuran20191701241@cis.asu.edu.eg";
            var EmailPassowrd = "nourana245@gmai";

            //smtp.gmail.com
            var client = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(SenderEmail, EmailPassowrd),
                EnableSsl = true
            };

            return client.SendMailAsync(new MailMessage(
                                                        from:SenderEmail,
                                                        to: receiverEmail,
                                                        subject: subject,
                                                        body: message
                                                        ));
        }
    }
}
