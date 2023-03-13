using Repository.Books;
using Repository.Books.Domain;

namespace Api.Books.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository repository;
        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IList<Book>> GetBooks()
        {
            return await repository.GetAllAsync();
        }

        public async Task<Book?> GetBook(int id)
        {
            return await repository.GetBookAsync(id);
        }

        public async Task SubscribeBook(int id)
        {
            var book = await repository.GetBookAsync(id);
            if (book != null)
            {
                repository.SubscribeBook(book);
            }
            else
            {
                throw new Exception("Book not found");
            }
        }

        public async Task UnsubscribeBook(int id)
        {
            var book = await repository.GetBookAsync(id);
            if (book != null)
            {
                repository.UnsubscribeBook(book);
            }
            else
            {
                throw new Exception("Book not found");
            }
        }
    }
}
