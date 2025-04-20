using BookStore.WebUI.Dtos.SubscriberDtos;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using BookStore.WebUI.Services;

namespace BookStore.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private LanguageService _localization;

      

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
         
          
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateSubscriber([FromBody] CreateSubscriberDto createSusbcriberDto)
        {
            if (string.IsNullOrWhiteSpace(createSusbcriberDto.Email))
            {
                return BadRequest(new { message = "Email is required" });
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSusbcriberDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7293/api/Subscribers", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Ok(new { message = "Subscribed successfully!" });
            }

            return StatusCode((int)responseMessage.StatusCode, new { message = "An error occurred" });
        }

        [HttpGet]
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddYears(10)
            });
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
