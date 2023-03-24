using Api.Books;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

//await app.StartAsync();
//Console.WriteLine($"Urls from Program.cs after app.StartAsync(): {string.Join(", ", app.Urls)}");

startup.Configure(app, builder.Environment);

app.Run();

//await app.WaitForShutdownAsync();