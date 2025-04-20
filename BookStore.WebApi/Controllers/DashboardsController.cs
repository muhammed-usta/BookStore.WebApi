using BookStore.BusinessLayer.Abstract;
using BookStore.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet("GetProductCount")]
        public IActionResult GetProductCount()
        {
            return Ok(_dashboardService.GetProductCount());
        }

        [HttpGet("GetAuthorCount")]
        public IActionResult GetAuthorCount()
        {
            return Ok(_dashboardService.GetAuthorCount());
        }

        [HttpGet("GetTotalStock")]
        public IActionResult GetTotalStock()
        {
            return Ok(_dashboardService.GetTotalStock());
        }

        [HttpGet("GetMostExpensiveProduct")]
        public IActionResult GetMostExpensiveProduct()
        {
            return Ok(_dashboardService.GetMostExpensiveProduct());
        }

        [HttpGet("GetLastAddedProduct")]
        public IActionResult GetLastAddedProduct()
        {
            return Ok(_dashboardService.GetLastAddedProduct());
        }

        [HttpGet("GetSubscriberCount")]
        public IActionResult GetSubscriberCount()
        {
            return Ok(_dashboardService.GetSubscriberCount());
        }

        [HttpGet("GetCategoryCount")]
        public IActionResult GetCategoryCount()
        {
            return Ok(_dashboardService.GetCategoryCount());
        }

        [HttpGet("GetAverageProductPrice")]
        public IActionResult GetAverageProductPrice()
        {
            return Ok(_dashboardService.GetAverageProductPrice());
        }

        [HttpGet("GetLeastStockProduct")]
        public IActionResult GetLeastStockProduct()
        {
            return Ok(_dashboardService.GetLeastStockProduct());
        }

        [HttpGet("GetProductWithLongestDescription")]
        public IActionResult GetProductWithLongestDescription()
        {
            return Ok(_dashboardService.GetProductWithLongestDescription());
        }

        [HttpGet("GetMostUsedEmailDomain")]
        public IActionResult GetMostUsedEmailDomain()
        {
            return Ok(_dashboardService.GetMostUsedEmailDomain());
        }

        [HttpGet("GetCategoryWithMostStock")]
        public IActionResult GetCategoryWithMostStock()
        {
            return Ok(_dashboardService.GetCategoryWithMostStock());
        }


        [HttpGet("GetCategoryProductCounts")]
        public IActionResult GetCategoryProductCounts()
        {
            var values = _dashboardService.GetCategoryProductCounts();
            return Ok(values);
        }

        [HttpGet("GetCategoryTotalPrices")]
        public IActionResult GetCategoryTotalPrices()
        {
            List<CategoryPriceDto> values = _dashboardService.GetCategoryTotalPrices();
            return Ok(values);
        }
    }
}
