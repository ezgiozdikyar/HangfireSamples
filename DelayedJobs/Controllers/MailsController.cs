using DelayedJobs.Business;
using Microsoft.AspNetCore.Mvc;

namespace DelayedJobs.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        [HttpPost]
        public void SendEmail(string email)
        {
            Hangfire.BackgroundJob.Schedule<MailManager>(
               j => j.SendMail(email),
               TimeSpan.FromSeconds(30));
        }
    }
}
