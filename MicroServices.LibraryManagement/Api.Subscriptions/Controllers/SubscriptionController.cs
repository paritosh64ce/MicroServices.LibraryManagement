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
        private readonly ILogger<SubscriptionController> logger;
        public SubscriptionController(ISubscriptionService service, IBookService bookService, ILogger<SubscriptionController> logger)
        {
            this.service = service;
            this.bookService = bookService;
            this.logger = logger;
        }

        [HttpGet("{subscriberName?}")]
        public async Task<IActionResult> Get(string? subscriberName = null)
        {
            var subscriptions = await service.GetSubscriptionsAsync(subscriberName);
            logger.LogInformation($"Subscriptions searched for {subscriberName}");
            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Subscription subscription)
        {
            var book = await bookService.GetBook(subscription.BookId);

            if (book == null)
            {
                logger.LogError($"Book with id {subscription.BookId} not found");
                return NotFound($"Book with id {subscription.BookId} not found");
            }
            else
            {
                await bookService.SubscribeBook(subscription.BookId);
                var newSubscription = await service.AddSubscriptionAsync(subscription);
                logger.LogInformation($"Subscription added for user: {subscription.SubscriberName} for Book with id: {subscription.BookId}.");
                return new CreatedResult("", newSubscription);
            }            
        }
    }
}
