using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Common
{
    public static class AppExtensions
    {
        public static IServiceCollection AddConsulConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration["ConsulConfig:Host"];
                consulConfig.Address = new Uri(address);
            }));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            //if (!(app.Properties["server.Features"] is FeatureCollection features))
            //{
            //    return app;
            //}

            //var addresses = features.Get<IServerAddressesFeature>();
            //var address = addresses.Addresses.First();
            // above code works when we do `await app.StartAsync();` from Program.cs, but api doesn't accept any request in that case
            // hence, have to get `ServiceAddress` from appsettings.json

            var serviceName = configuration["ConsulConfig:ServiceName"];

            var uri = new Uri(configuration["ConsulConfig:ServiceAddress"]);
            var serviceId = $"{serviceName}_{uri.Host}:{uri.Port}";

            var registration = new AgentServiceRegistration()
            {
                ID = serviceId,
                Name = serviceName,
                Address = uri.Host,
                Port = uri.Port
            };
            
            logger.LogWarning($"Registering with Consul with address: {configuration["ConsulConfig:ServiceAddress"]}");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogWarning("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}