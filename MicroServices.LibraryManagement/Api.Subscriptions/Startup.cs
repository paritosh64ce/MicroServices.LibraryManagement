using Api.Subscriptions.Services;
using Common;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using Repository.Subscriptions;
using Repository.Subscriptions.DbContext;

namespace Api.Books
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConsulConfig(Configuration);

            var connectionString = Configuration.GetConnectionString("SubscriptionDbConnection");
            services.AddDbContext<SubscriptionDbContext>(opts => opts.UseSqlServer(connectionString));

            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            services.AddHttpClient<IBookService, BookService>(c =>
            c.BaseAddress = new Uri(Configuration["BookServiceUrl"]))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // providing hardcoded portNumber for registration in Consul
            app.UseConsul(Configuration, "localhost", 5001);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(5,
                    retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(1.5, retryAttempt) * 1000),
                    (_, waitingTime) =>
                    {
                        Console.WriteLine("Retrying due to Polly retry policy");
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(15));
        }

    }
}
