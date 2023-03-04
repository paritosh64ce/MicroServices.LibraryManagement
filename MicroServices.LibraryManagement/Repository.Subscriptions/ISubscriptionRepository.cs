using Repository.Subscriptions.Domain;

namespace Repository.Subscriptions
{
    public interface ISubscriptionRepository
    {
        Task<List<Subscription>> GetAsync(string? subscriberName);
        Task<Subscription> AddSubscription(string subscriberName, int bookId);
        void UpdateSubscription(Subscription subscription);
    }
}