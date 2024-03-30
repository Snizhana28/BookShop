using Microsoft.EntityFrameworkCore;
using WPF.App3.Models;

namespace WPF.App3
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();
            user.HasIndex(x => x.Login).IsUnique();
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<WriteOfBook> WriteOfBooks { get; set; } = default!;
        public DbSet<PostponedBook> PostponedBooks { get; set; } = default!;
    }

}
