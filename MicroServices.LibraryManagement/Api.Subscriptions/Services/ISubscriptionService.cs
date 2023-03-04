using Repository.Subscriptions.Domain;

namespace Api.Subscriptions.Services
{
    public interface ISubscriptionService
    {
        Task<IList<Subscription>> GetSubscriptionsAsync(string? subscriberName);

        Task<Subscription> AddSubscriptionAsync(Subscription subscription);
    }
}