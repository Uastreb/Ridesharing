using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RideSharingApp.WEB.Helpers
{
    public static class SendEmail
    {
        public async static Task<bool> SendAsync(string recipient, string message, string body)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                var a = smtp.Credentials;
                MailMessage mess = new MailMessage("ridesharing@inbox.ru", recipient);

                mess.Body = body;
                mess.Subject = message;

                await smtp.SendMailAsync(mess);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Send(string recipient, string message, string body)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                var a = smtp.Credentials;
                MailMessage mess = new MailMessage("ridesharing@inbox.ru", recipient);
                
                mess.Body = body;
                mess.Subject = message;

                smtp.Send(mess);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}