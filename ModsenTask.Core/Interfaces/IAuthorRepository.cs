using ModsenTask.Core.Entities;

namespace ModsenTask.Infrastructure.Repositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task DeleteAsync(Guid authorId);
        Task<List<Author>> GetAllAsync();
        Task<List<Book>> GetBooksAsync(Guid authorId);
        Task<Author?> GetByIdAsync(Guid authorId);
        Task UpdateAsync(Author author);
    }
}