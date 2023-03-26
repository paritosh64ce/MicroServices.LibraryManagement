using Api.Books;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var logger = new LoggerConfiguration()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"Subscriptions-{DateTime.UtcNow:yyyy-MM}"
                    //IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}"
                })
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();
builder.Host.UseSerilog(logger);


var app = builder.Build();

app.UseSerilogRequestLogging();


//await app.StartAsync();
//Console.WriteLine($"Urls from Program.cs after app.StartAsync(): {string.Join(", ", app.Urls)}");

startup.Configure(app, builder.Environment);

app.Run();

//await app.WaitForShutdownAsync();