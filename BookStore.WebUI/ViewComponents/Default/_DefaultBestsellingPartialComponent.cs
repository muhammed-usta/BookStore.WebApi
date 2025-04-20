using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using BookStore.WebUI.Dtos.ProductDtos;
using System.Threading.Tasks;

namespace BookStore.WebUI.ViewComponents.Default
{
    public class _DefaultBestsellingPartialComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultBestsellingPartialComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7293/api/products/random");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var randomProduct = JsonConvert.DeserializeObject<ResultProductDto>(jsonData);
                return View(randomProduct);
            }

            return View();
        }
    }
}



