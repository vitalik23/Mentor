using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Mentor
{
    public class EmailService
    {

        public void SendEmailDefault(string email,string subject,string textMessage)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress("saminindima113@gmail.com", "Мой Email");
            message.To.Add(email);
            message.Subject = subject;
            message.Body = textMessage;

            // message.Attachments.Add(new Attachment("..путь к файлу...."));
            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
            {
                client.Credentials = new NetworkCredential("saminindima113@gmail.com", "0971461796");
                client.Port = 587;
                client.EnableSsl = true;

                client.Send(message);

            }


        }
    }
}
