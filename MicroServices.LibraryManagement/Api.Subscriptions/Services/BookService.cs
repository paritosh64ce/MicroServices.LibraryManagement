using Common;
using Newtonsoft.Json;
using Repository.Books.Domain;

namespace Api.Subscriptions.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;
        public BookService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<Book> GetBook(int id)
        {
            var response = await httpClient.GetAsync($"/api/book/{id}");
            return await response.ReadContentAs<Book>();
        }

        public async Task SubscribeBook(int id)
        {
            await httpClient.PostAsync($"/api/book/subscribe/{id}", null);
        }

        public Task UnsubscribeBook(int id)
        {
            throw new NotImplementedException();
        }
    }
}
