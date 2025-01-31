using AutoMapper;
using ModsenTask.Application.DTOs;
using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Services
{
    public class BookService(IBookRepository bookRepository, IMapper mapper) : IBookService
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<BookResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<List<BookResponse>>(books);
        }
        public async Task<BookResponse?> GetBookByIdAsync(Guid bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                return null;
            return _mapper.Map<BookResponse>(book);
        }
        public async Task<BookResponse?> GetBookByIsbnAsync(string isbn)
        {
            var book = await _bookRepository.GetBookByIsbnAsync(isbn);
            if (book == null)
                return null;
            return _mapper.Map<BookResponse>(book);
        }
        public async Task<(bool, Guid)> AddBookAsync(BookRequest bookRequest)
        {
            Book book = _mapper.Map<Book>(bookRequest);
            book.Id = Guid.NewGuid();
            bool result = await _bookRepository.AddBookAsync(book);
            if (!result)
                return (false, Guid.Empty);
            return (true, book.Id);
        }
        public async Task<bool> UpdateBookAsync(Guid bookId, BookRequest bookRequest)
        {
            Book book = _mapper.Map<Book>(bookRequest);
            book.Id = bookId;
            var existsBook = await _bookRepository.GetBookByIdAsync(bookId);
            if (existsBook == null)
                return false;
            await _bookRepository.UpdateBookAsync(book);
            return true;
        }
        public async Task<bool> DeleteBookAsync(Guid bookId)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                return false;

            await _bookRepository.DeleteBookAsync(bookId);
            return true;
        }
        public async Task<bool> LendBookAsync(Guid bookId, Guid userId, DateTime returnDate)
        {
            return await _bookRepository.LendBookAsync(bookId, userId, DateTime.UtcNow, returnDate);
        }

        public async Task<bool> UpdateBookImageAsync(Guid bookId, byte[] imageData)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId);
            if (book == null)
                return false;
            book.Image = imageData;
            await _bookRepository.UpdateBookAsync(book);
            return true;
        }
    }
}
