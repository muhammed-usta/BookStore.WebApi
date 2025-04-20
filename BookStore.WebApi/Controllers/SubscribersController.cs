using BookStore.BusinessLayer.Abstract;
using BookStore.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using BookStore.WebUI.Dtos.SubscriberDtos;
using BookStore.WebUI.Models;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        private readonly SmtpSettings _smtpSettings;

        public SubscribersController(ISubscriberService subscriberService, IConfiguration configuration)
        {
            _subscriberService = subscriberService;
            _smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

            Console.WriteLine("SMTP Ayarları: " + _smtpSettings?.SenderEmail);
            Console.WriteLine($"SMTP → Host: {_smtpSettings?.Host}, Email: {_smtpSettings?.SenderEmail}");

        }



        [HttpGet]
        public IActionResult SubscriberList()
        {
            return Ok(_subscriberService.TGetAll());
        }

        [HttpPost] 
        public IActionResult CreateSubscriber(Subscriber subscriber)
        {
            _subscriberService.TAdd(subscriber);
            return Ok("The addition operation was succesfull");
        }

        [HttpPut]

        public IActionResult UpdateSubscriber(Subscriber subscriber)
        {
            _subscriberService.TUpdate(subscriber);
            return Ok("The update operation was succesfull");
        }

        [HttpDelete]

        public IActionResult DeleteSubscriber(int id)
        {
            _subscriberService.TDelete(id);
            return Ok("The delete operation was succesfull");
        }

        [HttpGet("GetSubscriber")]

        public IActionResult GetSubscriber(int id)
        {
            return Ok(_subscriberService.TGetById(id));
        }
        
        [HttpPost("SendMailToAll")]
        public async Task<IActionResult> SendMailToAll()
        {
            var subscribers = _subscriberService.TGetAll();

            var htmlBody = @"
        <h2>📚 Welcome to BookSaw – You're Officially Subscribed!</h2>
        <p>Hello there,</p>

        <p>
            Thank you for joining the BookSaw newsletter! 🎉<br/>
            You're now part of our reading community and will receive updates about:
        </p>

        <ul>
            <li>🔔 The latest book releases</li>
            <li>💸 Exclusive subscriber-only deals</li>
            <li>📝 Hand-picked reading lists</li>
        </ul>

        <p>
            🔗 <strong><a href='https://booksaw.com' target='_blank'>Visit our website to discover new books!</a></strong>
        </p>

        <p>
            Happy reading! 📖<br/>
            — The BookSaw Team
        </p>
    ";
            if (_smtpSettings == null)
                throw new Exception("SMTP yapılandırması alınamadı. Lütfen appsettings.json içeriğini kontrol et.");

            foreach (var sub in subscribers)
            {
                await SendEmailAsync(sub.Email, "📚 Welcome to BookSaw!", htmlBody);
            }

            return Ok(new { message = "The email has been sent." });


        }




        private async Task SendEmailAsync(string to, string subject, string body)
        {
            var smtp = new SmtpClient(_smtpSettings.Host)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password),
                EnableSsl = _smtpSettings.EnableSsl
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            await smtp.SendMailAsync(mail);
        }


    }
}
