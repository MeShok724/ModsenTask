using Microsoft.EntityFrameworkCore;
using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Persistence;

namespace ModsenTask.Infrastructure.Repositories
{
    public class BookRepository(AppDbContext appDbContext) : IBookRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .ToListAsync();
        }
        public async Task<Book?> GetBookByIdAsync(Guid bookId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public async Task<Book?> GetBookByIsbnAsync(string isbn)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.ISBN == isbn);
        }
        public async Task<bool> AddBookAsync(Book book)
        {
            var authorExists = await _context.Authors
                .FindAsync(book.AuthorId);
            if (authorExists == null)
                return false;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(Guid bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> LendBookAsync(Guid bookId, Guid userId, DateTime lendingDate, DateTime returnDate)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.IsTaken)
                return false;

            var userBook = new UserBook
            {
                UserId = userId,
                BookId = bookId,
                TakenAt = lendingDate,
                ReturnBy = returnDate
            };

            book.IsTaken = true;
            _context.UserBooks.Add(userBook);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task UpdateBookImageAsync(int bookId, byte[] imageData)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.Image = imageData;
                await _context.SaveChangesAsync();
            }
        }
    }
}
