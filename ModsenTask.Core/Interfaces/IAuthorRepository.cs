using ModsenTask.Core.Entities;

namespace ModsenTask.Infrastructure.Repositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task DeleteAsync(int authorId);
        Task<List<Author>> GetAllAsync();
        Task<List<Book>> GetBooksAsync(int authorId);
        Task<Author?> GetByIdAsync(int authorId);
        Task UpdateAsync(Author author);
    }
}