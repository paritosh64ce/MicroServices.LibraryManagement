using Repository.Books.Domain;

namespace Repository.Books
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetBookAsync(int id);
        Task SubscribeBook(Book book);
        Task UnsubscribeBook(Book book);

    }
}