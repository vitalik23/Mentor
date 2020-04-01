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
            try
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
            catch (Exception ex)
            {

            }

            //var emailMessage = new MimeMessage();

            //emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@admin.com"));
            //emailMessage.To.Add(new MailboxAddress("", email));
            //emailMessage.Subject = subject;
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            //{
            //    Text = message
            //};

            //using (var client = new SmtpClient())
            //{
            //    await client.ConnectAsync("smtp.gmail.com", 465, true);
            //    await client.AuthenticateAsync("saminindima113@gmail.com", "0971461796");/**/
            //    await client.SendAsync(emailMessage);

            //    await client.DisconnectAsync(true);
            //}
        }
    }
}
