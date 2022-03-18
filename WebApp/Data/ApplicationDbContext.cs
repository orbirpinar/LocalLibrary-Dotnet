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

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookInstance> BookInstance { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Language> Language { get; set; }

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