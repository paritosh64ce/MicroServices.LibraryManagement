using Common;
using Consul;
using Newtonsoft.Json;
using Repository.Books.Domain;

namespace Api.Subscriptions.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;
        private readonly IConsulClient consulClient;
        private readonly ILogger<BookService> logger;
        private const string BookServiceName = "bookService";
        public BookService(HttpClient httpClient, IConsulClient consulClient, ILoggerFactory loggerFactory)
        {
            this.httpClient = httpClient;
            this.consulClient = consulClient;
            logger = loggerFactory.CreateLogger<BookService>();
        }

        public async Task<Book> GetBook(int id)
        {
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = SelectAndGetBookServiceBaseAddress();
            
            var response = await httpClient.GetAsync($"/api/book/{id}");
            return await response.ReadContentAs<Book>();
        }

        public async Task SubscribeBook(int id)
        {
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = SelectAndGetBookServiceBaseAddress();
            
            await httpClient.PostAsync($"/api/book/subscribe/{id}", null);
        }

        public Task UnsubscribeBook(int id)
        {
            throw new NotImplementedException();
        }

        private Uri SelectAndGetBookServiceBaseAddress()
        {
            var bookServices = this.consulClient.Agent.Services().Result.Response.Where(x => x.Value.Service.Equals(BookServiceName, StringComparison.OrdinalIgnoreCase)).Select(x => x.Value).ToList();
            if (bookServices.Count > 0)
            {
                Random random = new Random();
                var bookSvcToCall = bookServices[random.Next(0, bookServices.Count)];

                logger.LogWarning($"BookService {bookSvcToCall.ID} selected for the current request");
                return new Uri(bookSvcToCall.ID.Replace($"{BookServiceName}_", string.Empty));
            }
            else
            {
                logger.LogCritical("No BookService is available to accept the request");
                return null;
            }
        }

    }
}
