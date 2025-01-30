using Microsoft.EntityFrameworkCore;
using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Persistence;

namespace ModsenTask.Infrastructure.Repositories
{
    public class AuthorRepository(AppDbContext appDbContext) : IAuthorRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(Guid authorId)
        {
            return await _context.Authors.FindAsync(authorId);
        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetBooksAsync(Guid authorId)
        {
            return await _context.Books
                .Where(book => book.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
