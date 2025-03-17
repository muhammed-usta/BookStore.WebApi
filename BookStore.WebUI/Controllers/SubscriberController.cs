using BookStore.WebUI.Dtos.ProductDtos;
using BookStore.WebUI.Dtos.QuoteDtos;
using BookStore.WebUI.Dtos.SubscriberDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BookStore.WebUI.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubscriberController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> SubscriberList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7293/api/Subscribers");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSusbcriberDto>>(jsonData);
                return View(values);
            }

            return View();

        }

        [HttpGet]
        public IActionResult CreateSubscriber()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateSubscriber(CreateSusbcriberDto createSusbcriberDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createSusbcriberDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7293/api/Subscribers", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SubscriberList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubscriber(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7293/api/Subscribers/GetSubscribe?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdQuoteDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscriber(UpdateSusbcriberDto updateSusbcriberDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateSusbcriberDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7293/api/Subscribers", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("SubscriberList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {

            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7293/api/Subscribers?id=" + id);
            return RedirectToAction("SubscriberList");
        }
    }


}
