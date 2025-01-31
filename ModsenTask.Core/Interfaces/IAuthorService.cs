using ModsenTask.Application.DTOs;
using ModsenTask.Core.Entities;

namespace ModsenTask.Application.Services
{
    public interface IAuthorService
    {
        Task<Guid> AddAuthorAsync(AuthorRequest author);
        Task<bool> DeleteAuthorAsync(Guid authorId);
        Task<List<AuthorResponse>> GetAllAuthorsAsync();
        Task<AuthorResponse?> GetAuthorByIdAsync(Guid authorId);
        Task<List<BookResponse>> GetBooksByAuthorAsync(Guid authorId);
        Task<bool> UpdateAuthorAsync(Guid authorId, AuthorRequest authorRequest);
    }
}