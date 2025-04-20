using BookStore.BusinessLayer.Abstract;
using BookStore.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            return Ok(_productService.TGetAll());
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _productService.TAdd(product);
            return Ok("The addition operation was successful");
        }
        
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            _productService.TUpdate(product);
            return Ok("The update operation was successful");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            _productService.TDelete(id);
            return Ok("The delete operation was successful");
        }


        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            return Ok(_productService.TGetById(id));
        }
        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            return Ok(_productService.TGetProductCount());
        }

        [HttpGet("random")]
        public IActionResult GetRandomProduct()
        {
            var randomBook = _productService.GetRandomProduct();
            if (randomBook == null)
            {
                return NotFound(new { message = "No products available" });
            }
            return Ok(randomBook);
        }
        [HttpGet("GetBooksByCategory/{categoryId}")]
        public IActionResult GetBooksByCategory(int categoryId)
        {
            var books = _productService
                .TGetAllWithCategory()
                .Where(p => p.CategoryId == categoryId)
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Author,
                    p.Description,
                    p.Price,
                    p.ImageUrl,
                    CategoryName = p.Category.CategoryName
                }).ToList();

            return Ok(books);
        }


        [HttpGet("GetCategoriesWithBooks")]
        public IActionResult GetCategoriesWithBooks()
        {
            var categories = _productService
                .TGetAllWithCategory()
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    Products = g.Select(p => new
                    {
                        p.ProductId,
                        p.Name,
                        p.Author,
                        p.Description,
                        p.Price,
                        p.ImageUrl
                    }).ToList()
                }).ToList();

            return Ok(categories);
        }









    }
}
