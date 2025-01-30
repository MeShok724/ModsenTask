using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Services
{
    public class BookService(IBookRepository bookRepository)
    {
        private readonly IBookRepository _bookRepository = bookRepository;

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.GetBookByIdAsync(bookId);
        }
        public async Task<Book?> GetBookByIsbnAsync(string isbn)
        {
            return await _bookRepository.GetBookByIsbnAsync(isbn);
        }
        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddBookAsync(book);
        }
        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateBookAsync(book);
        }
        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                return false;

            await _bookRepository.DeleteBookAsync(bookId);
            return true;
        }
        public async Task<bool> LendBookAsync(int bookId, Guid userId, DateTime returnDate)
        {
            return await _bookRepository.LendBookAsync(bookId, userId, returnDate);
        }
        public async Task<bool> UpdateBookImageAsync(int bookId, byte[] imageData)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                return false;

            await _bookRepository.UpdateBookImageAsync(bookId, imageData);
            return true;
        }
    }
}
