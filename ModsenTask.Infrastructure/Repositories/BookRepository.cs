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
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }
        public async Task<Book?> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == bookId);
        }
        public async Task<Book?> GetBookByIsbnAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }
        public async Task AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> LendBookAsync(int bookId, Guid userId, DateTime returnDate)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.IsTaken)
                return false;

            var userBook = new UserBook
            {
                UserId = userId,
                BookId = bookId,
                TakenAt = DateTime.UtcNow,
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
