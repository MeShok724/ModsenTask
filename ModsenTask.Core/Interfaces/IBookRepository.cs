using ModsenTask.Core.Entities;

namespace ModsenTask.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task DeleteBookAsync(int bookId);
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int bookId);
        Task<Book?> GetBookByIsbnAsync(string isbn);
        Task<bool> LendBookAsync(int bookId, Guid userId, DateTime returnDate);
        Task UpdateBookAsync(Book book);
        Task UpdateBookImageAsync(Guid bookId, byte[] imageData);
    }
}