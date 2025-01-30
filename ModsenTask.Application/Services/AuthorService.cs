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
    public class AuthorService(IAuthorRepository authorRepository, IMapper mapper) : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<List<AuthorResponse>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<List<AuthorResponse>>(authors);
        }

        public async Task<AuthorResponse?> GetAuthorByIdAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
                return null;
            return _mapper.Map<AuthorResponse>(author);
        }

        public async Task<Guid> AddAuthorAsync(AuthorRequest authorRequest)
        {
            var author = _mapper.Map<Author>(authorRequest);
            author.Id = Guid.NewGuid();

            await _authorRepository.AddAsync(author);
            return author.Id;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(author.Id);
            if (existingAuthor == null)
                return false;

            await _authorRepository.UpdateAsync(author);
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(Guid authorId)
        {
            var author = await _authorRepository.GetByIdAsync(authorId);
            if (author == null)
                return false;

            await _authorRepository.DeleteAsync(authorId);
            return true;
        }

        public async Task<List<BookResponse>> GetBooksByAuthorAsync(Guid authorId)
        {
            var books = await _authorRepository.GetBooksAsync(authorId);
            return _mapper.Map<List<BookResponse>>(books);
        }
    }
}
