using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using BackEnd.BAL;
using BackEnd.Entity;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly BALEmail _balEmail;
        public EmailController(BALEmail balEmail)
        {
            _balEmail = balEmail;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDto request)
        {
            _balEmail.SendEmail(request);
            return Ok();
        }
    }
}
