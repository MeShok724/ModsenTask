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
            var authorToUpdate = await _context.Authors
                .FindAsync(author.Id);
            if (authorToUpdate == null)
                return;

            authorToUpdate.FirstName = author.FirstName;
            authorToUpdate.LastName = author.LastName;
            authorToUpdate.DateOfBirth = author.DateOfBirth;
            authorToUpdate.Country = author.Country;

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
