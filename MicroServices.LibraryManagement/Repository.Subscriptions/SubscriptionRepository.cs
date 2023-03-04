using Microsoft.EntityFrameworkCore;
using Repository.Subscriptions.DbContext;
using Repository.Subscriptions.Domain;
using System.Linq;

namespace Repository.Subscriptions
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly SubscriptionDbContext _dbContext;

        public SubscriptionRepository(SubscriptionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Subscription>> GetAsync(string? subscriberName)
        {
            IQueryable<Subscription> query = this._dbContext.Subscriptions.AsQueryable();
            if (!string.IsNullOrEmpty(subscriberName))
            {
                query = query.Where(x => x.SubscriberName == subscriberName);
            }
            return await query.ToListAsync();
        }

        public async Task<Subscription> AddSubscription(string subscriberName, int bookId)
        {
            var subscription = new Subscription(subscriberName, bookId);
            _dbContext.Subscriptions.Add(subscription);
            await _dbContext.SaveChangesAsync();
            return subscription;
        }

        public async void UpdateSubscription(Subscription subscription)
        {
            var sub = _dbContext.Subscriptions.FirstOrDefault(x => x.Id == subscription.Id);
            if (sub != null)
            {
                sub.DateReturned = subscription.DateReturned;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}