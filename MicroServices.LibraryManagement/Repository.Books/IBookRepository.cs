using Repository.Books.Domain;

namespace Repository.Books
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetBookAsync(int id);
        void SubscribeBook(Book book);
        void UnsubscribeBook(Book book);

    }
}