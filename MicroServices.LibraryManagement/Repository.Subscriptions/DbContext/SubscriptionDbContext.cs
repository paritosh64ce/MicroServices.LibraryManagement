using Microsoft.EntityFrameworkCore;
using Repository.Subscriptions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Subscriptions.DbContext
{
    public class SubscriptionDbContext : Microsoft.EntityFrameworkCore.DbContext, IDisposable
    {
        public SubscriptionDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription("Paritosh", 1) { Id = 1 },
                new Subscription("SecondUser", 2) { Id = 2 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
