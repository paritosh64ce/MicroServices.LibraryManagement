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
        public SubscriptionController(ISubscriptionService service)
        {
            this.service = service;
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
            var newSubscription = await service.AddSubscriptionAsync(subscription);
            return new CreatedResult("", newSubscription);
        }
    }
}
