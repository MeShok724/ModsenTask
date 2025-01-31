using ModsenTask.Application.DTOs;
using ModsenTask.Core.Entities;

namespace ModsenTask.Application.Services
{
    public interface IBookService
    {
        Task<(bool, Guid)> AddBookAsync(BookRequest bookRequest);
        Task<bool> DeleteBookAsync(Guid bookId);
        Task<List<BookResponse>> GetAllBooksAsync();
        Task<BookResponse?> GetBookByIdAsync(Guid bookId);
        Task<BookResponse?> GetBookByIsbnAsync(string isbn);
        Task<bool> LendBookAsync(Guid bookId, Guid userId, DateTime returnDate);
        Task<bool> UpdateBookAsync(Guid bookId, BookRequest bookRequest);
        Task<bool> UpdateBookImageAsync(Guid bookId, byte[] imageData);
    }
}