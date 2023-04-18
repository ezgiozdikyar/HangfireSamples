using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace DelayedJobs.Business
{
    public class MailManager
    {

        private readonly IConfiguration _configuration;
        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMail(string email)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            using (var client = new SmtpClient(emailSettings["Host"], int.Parse(emailSettings["Port"])))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailSettings["UserName"], emailSettings["Password"]);
                client.EnableSsl = true;

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(emailSettings["UserName"]);
                    message.To.Add(email);
                    message.Subject = "Confirm your account";
                    message.Body = "Please click the link below to confirm your account.";

                    client.SendMailAsync(message);
                }
            }
        }
    }
}
