using BookStore.WebUI.Dtos.CategoryDtos;
using BookStore.WebUI.Dtos.ProductDtos;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

      
        private async Task LoadCategoriesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7293/api/Categories");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

                if (categoryList != null && categoryList.Any())
                {
                    ViewBag.Categories = new SelectList(categoryList, "CategoryId", "CategoryName");
                }
                else
                {
                    ViewBag.Categories = new SelectList(new List<ResultCategoryDto>(), "CategoryId", "CategoryName"); 
                }
            }
            else
            {
                ViewBag.Categories = new SelectList(new List<ResultCategoryDto>(), "CategoryId", "CategoryName"); 
            }
        }

        public async Task<IActionResult> ProductList()
        {
            var client = _httpClientFactory.CreateClient();

            var categoryResponse = await client.GetAsync("https://localhost:7293/api/Categories");
            var categoryDictionary = new Dictionary<int, string>();

            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonCategoryData = await categoryResponse.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonCategoryData);

                foreach (var category in categoryList)
                {
                    categoryDictionary[category.CategoryId] = category.CategoryName;
                }
            }

            ViewBag.CategoryDictionary = categoryDictionary;

            var productResponse = await client.GetAsync("https://localhost:7293/api/Products");
            var productList = new List<ResultProductDto>();

            if (productResponse.IsSuccessStatusCode)
            {
                var jsonData = await productResponse.Content.ReadAsStringAsync();
                productList = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
            }

            return View(productList);
        }


        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            await LoadCategoriesAsync(); 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            if (createProductDto.ImageFile != null)
            {
                createProductDto.ImageUrl = await SaveImageAsync(createProductDto.ImageFile);
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createProductDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7293/api/Products", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            await LoadCategoriesAsync();

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7293/api/Products/GetProduct?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdProductDto>(jsonData);
                return View(values);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.ImageFile != null)
            {
                updateProductDto.ImageUrl = await SaveImageAsync(updateProductDto.ImageFile);
            }
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateProductDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7293/api/Products", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7293/api/Products?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View();
        }
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

         
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
            {
                throw new ValidationException("The file format must be either .jpg, .jpeg, or .png.");
            }

        
            var imageName = Guid.NewGuid() + extension;

            var imageFolder = Path.Combine(currentDirectory, "wwwroot/images");
            if (!Directory.Exists(imageFolder))
            {
                Directory.CreateDirectory(imageFolder);
            }

            var saveLocation = Path.Combine(imageFolder, imageName);

            using (var stream = new FileStream(saveLocation, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/images/" + imageName;
        }

       

    }
}
