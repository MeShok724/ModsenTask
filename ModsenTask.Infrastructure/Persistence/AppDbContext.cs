using Microsoft.EntityFrameworkCore;
using ModsenTask.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModsenTask.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>()
                .HasKey(ub => new { ub.UserId, ub.BookId });

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.BorrowedBooks)
                .HasForeignKey(ub => ub.UserId);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.Book)
                .WithMany()
                .HasForeignKey(ub => ub.BookId);
        }
    }
}
