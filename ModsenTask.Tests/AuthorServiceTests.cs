using AutoMapper;
using ModsenTask.Application.DTOs;
using ModsenTask.Application.Services;
using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Tests
{
    public class AuthorServiceTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;
        private readonly IMapper _mapper;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorResponse>();
                cfg.CreateMap<AuthorRequest, Author>();
                cfg.CreateMap<Book, BookResponse>();
            });
            _mapper = mapperConfig.CreateMapper();

            _authorService = new AuthorService(_authorRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAuthorsAsync_ShouldReturnMappedAuthors()
        {
            var authors = new List<Author>
            {
                new() { Id = Guid.NewGuid(), FirstName = "Leo", LastName = "Tolstoy", Country = "Russia" }
            };
            _authorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(authors);

            var result = await _authorService.GetAllAuthorsAsync();

            Assert.Single(result);
            Assert.Equal(authors[0].FirstName, result[0].FirstName);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnMappedAuthor_WhenExists()
        {
            var author = new Author { Id = Guid.NewGuid(), FirstName = "George", LastName = "Orwell", Country = "UK" };
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(author.Id)).ReturnsAsync(author);

            var result = await _authorService.GetAuthorByIdAsync(author.Id);

            Assert.NotNull(result);
            Assert.Equal(author.FirstName, result.FirstName);
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Author?)null);

            var result = await _authorService.GetAuthorByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task AddAuthorAsync_ShouldReturnNewGuid()
        {
            var authorRequest = new AuthorRequest { FirstName = "Leo", LastName = "Tolstoy" };
            Guid authorId = Guid.NewGuid();
            _authorRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Author>())).Returns(Task.CompletedTask);

            var result = await _authorService.AddAuthorAsync(authorRequest);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnTrue_WhenAuthorExists()
        {
            var authorId = Guid.NewGuid();
            var authorRequest = new AuthorRequest { FirstName = "Updated", LastName = "Author" };
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId)).ReturnsAsync(new Author { Id = authorId });

            var result = await _authorService.UpdateAuthorAsync(authorId, authorRequest);

            Assert.True(result);
            _authorRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Author>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAuthorAsync_ShouldReturnFalse_WhenAuthorDoesNotExist()
        {
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Author?)null);

            var result = await _authorService.UpdateAuthorAsync(Guid.NewGuid(), new AuthorRequest());

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldReturnTrue_WhenAuthorExists()
        {
            var authorId = Guid.NewGuid();
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(authorId)).ReturnsAsync(new Author { Id = authorId });

            var result = await _authorService.DeleteAuthorAsync(authorId);

            Assert.True(result);
            _authorRepositoryMock.Verify(repo => repo.DeleteAsync(authorId), Times.Once);
        }

        [Fact]
        public async Task DeleteAuthorAsync_ShouldReturnFalse_WhenAuthorDoesNotExist()
        {
            _authorRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Author?)null);

            var result = await _authorService.DeleteAuthorAsync(Guid.NewGuid());

            Assert.False(result);
        }

        [Fact]
        public async Task GetBooksByAuthorAsync_ShouldReturnMappedBooks()
        {
            var authorId = Guid.NewGuid();
            var books = new List<Book>
            {
                new() { Id = Guid.NewGuid(), Title = "War and Peace", AuthorId = authorId },
                new() { Id = Guid.NewGuid(), Title = "Anna Karenina", AuthorId = authorId }
            };
            _authorRepositoryMock.Setup(repo => repo.GetBooksAsync(authorId)).ReturnsAsync(books);

            var result = await _authorService.GetBooksByAuthorAsync(authorId);

            Assert.Equal(books.Count, result.Count);
            Assert.Equal(books[0].Title, result[0].Title);
        }
    }
}
