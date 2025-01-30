using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Application.Services
{
    public class AuthorService(IAuthorRepository authorRepository)
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int authorId)
        {
            return await _authorRepository.GetByIdAsync(authorId);
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(author.Id);
            if (existingAuthor == null)
                return false;

            await _authorRepository.UpdateAsync(author);
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
                return false;

            await _authorRepository.DeleteAsync(authorId);
            return true;
        }

        public async Task<List<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _authorRepository.GetBooksAsync(authorId);
        }
    }
}
