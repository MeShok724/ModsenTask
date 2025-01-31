using ModsenTask.Core.Entities;

namespace ModsenTask.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        Task<bool> AddBookAsync(Book book);
        Task DeleteBookAsync(Guid bookId);
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(Guid bookId);
        Task<Book?> GetBookByIsbnAsync(string isbn);
        Task<bool> LendBookAsync(Guid bookId, Guid userId, DateTime lendingDate, DateTime returnDate);
        Task UpdateBookAsync(Book book);
    }
}