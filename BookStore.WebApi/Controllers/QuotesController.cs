using BookStore.BusinessLayer.Abstract;
using BookStore.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuotesController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]

        public IActionResult QuoteList()
        {
            return Ok(_quoteService.TGetAll());
        }

        [HttpPost]
        public IActionResult CreateQuote(Quote quote)
        {
            _quoteService.TAdd(quote);
            return Ok("The addition operation was successful");
        }

        [HttpPut]

        public IActionResult UpdateQuote(Quote quote)
        {

            _quoteService.TUpdate(quote);
            return Ok("The update operation was successful");
        }

        [HttpDelete]
        public IActionResult DeleteQuote(int id)
        {
            _quoteService.TDelete(id);
            return Ok("The delete operation was successful");
        }

        [HttpGet("GetQuote")]
        public IActionResult GetQuote(int id)
        {

            return Ok(_quoteService.TGetById(id));
        }
    }
}
