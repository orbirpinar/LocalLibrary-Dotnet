using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Author { get; set; } = null!;
        public DbSet<Book> Book { get; set; } = null!;
        public DbSet<BookInstance> BookInstance { get; set; } = null!;
        public DbSet<Genre> Genre { get; set; } = null!;
        public DbSet<Language> Language { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>().HasIndex(b => b.Title).IsUnique();
            builder.Entity<Author>().HasIndex(a => a.Name).IsUnique();
            builder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();
            builder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books);
            builder.Entity<BookInstance>(entity =>
                {
                    entity.Property(e => e.LoanStatus)
                        .HasConversion(x => (int) x, x => (LoanStatus) x);
                }
            );
        }
    }
}