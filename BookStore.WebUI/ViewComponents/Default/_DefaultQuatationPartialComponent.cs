using BookStore.WebUI.Dtos.QuoteDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.WebUI.ViewComponents.Default
{
    public class _DefaultQuatationPartialComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DefaultQuatationPartialComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
          var client=_httpClientFactory.CreateClient();
            var responsemessage = await client.GetAsync("https://localhost:7293/api/Quotes/LastQuote");
            if (responsemessage.IsSuccessStatusCode)
            {
                var jsonData=await responsemessage.Content.ReadAsStringAsync();
                var lastQuote=JsonConvert.DeserializeObject<ResultQuoteDto>(jsonData);
                return View(lastQuote);
            }
            return View();
        }
    }
}
