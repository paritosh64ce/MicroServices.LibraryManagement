using Api.Subscriptions.Services;
using Microsoft.AspNetCore.Mvc;
using Repository.Subscriptions.Domain;

namespace Api.Subscriptions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService service;
        private readonly IBookService bookService;
        public SubscriptionController(ISubscriptionService service, IBookService bookService)
        {
            this.service = service;
            this.bookService = bookService;
        }

        [HttpGet("{subscriberName?}")]
        public async Task<IActionResult> Get(string? subscriberName = null)
        {
            var subscriptions = await service.GetSubscriptionsAsync(subscriberName);
            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Subscription subscription)
        {
            var book = await bookService.GetBook(subscription.BookId);

            if (book == null)
            {
                return NotFound($"Book with id {subscription.BookId} not found");
            }
            else
            {
                await bookService.SubscribeBook(subscription.BookId);
                var newSubscription = await service.AddSubscriptionAsync(subscription);
                return new CreatedResult("", newSubscription);
            }            
        }
    }
}
