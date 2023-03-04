using Api.Subscriptions.Services;
using Common;
using Microsoft.EntityFrameworkCore;
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

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // app.UseConsul(Configuration);
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

    }
}
