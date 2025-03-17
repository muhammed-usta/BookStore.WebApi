using BookStore.BusinessLayer.Abstract;
using BookStore.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;

        public SubscribersController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
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
    }
}
