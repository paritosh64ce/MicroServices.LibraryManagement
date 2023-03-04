using Repository.Subscriptions;
using Repository.Subscriptions.Domain;

namespace Api.Subscriptions.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository repository;
        public SubscriptionService(ISubscriptionRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IList<Subscription>> GetSubscriptionsAsync(string? subscriberName)
        {
            return await repository.GetAsync(subscriberName);
        }

        public async Task<Subscription> AddSubscriptionAsync(Subscription subscription)
        {
            return await repository.AddSubscription(subscription.SubscriberName, subscription.BookId);
        }
    }
}
