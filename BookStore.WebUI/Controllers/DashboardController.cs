using BookStore.EntityLayer.Concrete;
using BookStore.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace BookStore.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Dashboard()
        {
            var client = _httpClientFactory.CreateClient();

            var response1 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetProductCount");
            if (response1.IsSuccessStatusCode)
                ViewBag.productCount = await response1.Content.ReadAsStringAsync();

            var response2 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetAuthorCount");
            if (response2.IsSuccessStatusCode)
                ViewBag.authorCount = await response2.Content.ReadAsStringAsync();

            var response3 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetTotalStock");
            if (response3.IsSuccessStatusCode)
                ViewBag.totalStock = await response3.Content.ReadAsStringAsync();

            var response4 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetMostExpensiveProduct");
            if (response4.IsSuccessStatusCode)
                ViewBag.expensiveProduct = JsonConvert.DeserializeObject<Product>(await response4.Content.ReadAsStringAsync());

            var response5 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetLastAddedProduct");
            if (response5.IsSuccessStatusCode)
                ViewBag.lastProduct = JsonConvert.DeserializeObject<Product>(await response5.Content.ReadAsStringAsync());

            var response6 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetSubscriberCount");
            if (response6.IsSuccessStatusCode)
                ViewBag.emailCount = await response6.Content.ReadAsStringAsync();

            var response7 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetCategoryCount");
            if (response7.IsSuccessStatusCode)
                ViewBag.categoryCount = await response7.Content.ReadAsStringAsync();

            var response8 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetAverageProductPrice");
            if (response8.IsSuccessStatusCode)
            {
                var avgPriceStr = await response8.Content.ReadAsStringAsync();
                var avgPrice = Convert.ToDecimal(avgPriceStr.Replace(",", "."), CultureInfo.InvariantCulture);
                ViewBag.avgPrice = avgPrice.ToString("C2", new CultureInfo("tr-TR"));
            }

            var response9 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetLeastStockProduct");
            if (response9.IsSuccessStatusCode)
                ViewBag.leastStockProduct = JsonConvert.DeserializeObject<Product>(await response9.Content.ReadAsStringAsync());

            var response10 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetProductWithLongestDescription");
            if (response10.IsSuccessStatusCode)
                ViewBag.longestDescriptionProduct = JsonConvert.DeserializeObject<Product>(await response10.Content.ReadAsStringAsync());

            var response11 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetMostUsedEmailDomain");
            if (response11.IsSuccessStatusCode)
                ViewBag.mostUsedEmailDomain = await response11.Content.ReadAsStringAsync();

            var response12 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetCategoryWithMostStock");
            if (response12.IsSuccessStatusCode)
                ViewBag.categoryWithMostStock = JsonConvert.DeserializeObject<CategoryStockDto>(await response12.Content.ReadAsStringAsync());


            var response13 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetCategoryProductCounts");
            if (response13.IsSuccessStatusCode)
                ViewBag.getCategoryWithProducts = JsonConvert.DeserializeObject<List<ResultCategoryProductCountDto>>(await response13.Content.ReadAsStringAsync());


            var response14 = await client.GetAsync("https://localhost:7293/api/Dashboards/GetCategoryTotalPrices");
            if (response14.IsSuccessStatusCode)
                ViewBag.getCategoryTotalPrices=JsonConvert.DeserializeObject<List<CategoryPriceDto>>(await response14.Content.ReadAsStringAsync());
            return View();
        }
    }
}
