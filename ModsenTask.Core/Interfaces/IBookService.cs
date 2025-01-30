using ModsenTask.Application.DTOs;
using ModsenTask.Core.Entities;

namespace ModsenTask.Application.Services
{
    public interface IBookService
    {
        Task<Guid> AddBookAsync(BookRequest bookRequest);
        Task<bool> DeleteBookAsync(Guid bookId);
        Task<List<BookResponse>> GetAllBooksAsync();
        Task<BookResponse?> GetBookByIdAsync(Guid bookId);
        Task<BookResponse?> GetBookByIsbnAsync(string isbn);
        Task<bool> LendBookAsync(Guid bookId, Guid userId, DateTime returnDate);
        Task UpdateBookAsync(Book book);
    }
}