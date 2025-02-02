using Microsoft.EntityFrameworkCore;
using ModsenTask.Core.Entities;
using ModsenTask.Infrastructure.Persistence;
using ModsenTask.Infrastructure.Repositories;

namespace ModsenTask.Tests
{
    public class AuthorRepositoryTests
    {
        private AppDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }
        [Fact]
        public async Task AddAsync_ShouldAddAuthor()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new AuthorRepository(dbContext);
            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Leo",
                LastName = "Tolstoy",
                DateOfBirth = new DateTime(1828, 9, 9),
                Country = "Russia"
            };

            await repository.AddAsync(author);
            var result = await dbContext.Authors.FindAsync(author.Id);

            Assert.NotNull(result);
            Assert.Equal("Leo", result.FirstName);
            Assert.Equal("Tolstoy", result.LastName);
            Assert.Equal(new DateTime(1828, 9, 9), result.DateOfBirth);
            Assert.Equal("Russia", result.Country);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnAuthor_WhenExists()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new AuthorRepository(dbContext);
            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Leo",
                LastName = "Tolstoy",
                DateOfBirth = new DateTime(1828, 9, 9),
                Country = "Russia"
            };

            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();
            var result = await repository.GetByIdAsync(author.Id);

            Assert.NotNull(result);
            Assert.Equal(author.Id, result.Id);
            Assert.Equal("Leo", result.FirstName);
            Assert.Equal("Tolstoy", result.LastName);
            Assert.Equal(new DateTime(1828, 9, 9), result.DateOfBirth);
            Assert.Equal("Russia", result.Country);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            var dbContext = GetInMemoryDbContext();
            var repository = new AuthorRepository(dbContext);

            var result = await repository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }
    }
}