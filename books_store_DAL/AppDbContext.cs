using books_store_DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace books_store_DAL
{
    public class AppDbContext: DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<BookEntityGenreEntity> BookEntityGenreEntity {  get; set; }
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //required
            modelBuilder.Entity<BookEntity>().Property(b => b.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<AuthorEntity>().Property(a => a.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<GenreEntity>().Property(g => g.Name).IsRequired().HasMaxLength(100);

            //friendship
            modelBuilder.Entity<BookEntity>().HasOne(b => b.Author).WithMany(a => a.Books);
            //modelBuilder.Entity<BookEntity>().HasMany(b => b.Genres).WithMany(g => g.Books);
            modelBuilder.Entity<BookEntityGenreEntity>().HasKey(k => new { k.BookId, k.GenreId});

            //Other data type and default parameters 
            modelBuilder.Entity<BookEntity>(e =>
            {
                e.Property(b => b.Description).HasColumnType("text");
                e.Property(b => b.Image).HasMaxLength(200);
                e.Property(b => b.Pages).HasDefaultValue(0);
                e.Property(b => b.Rating).HasDefaultValue(0);

            });
            modelBuilder.Entity<AuthorEntity>().Property(a => a.Image).HasMaxLength(100);
            //---------------------------------------------------------------------------------
            var books = new List<BookEntity>
            {
                new BookEntity {Id = 1, Title = "Harry Potter and the Philoosopher's Stone", Pages = 300, PublishedYear = 1997, AuthorId = 1},
                new BookEntity {Id = 2, Title = "A game of thrones", Pages = 300, PublishedYear = 1996, AuthorId = 2},
                new BookEntity {Id = 3, Title = "The Hobbit", Pages = 300, PublishedYear = 1937, AuthorId = 3},
            };
            //---------------------------------------------------------------------------------
            var authors = new List<AuthorEntity>
            {
                new AuthorEntity {Id = 1, Name = "J.K. Rowling", BirthDate = new DateTime(1965, 7, 31) },
                new AuthorEntity {Id = 2, Name = "George R.R. Martin", BirthDate = new DateTime(1948, 9, 20) },
                new AuthorEntity {Id = 3, Name = "J.R.R. Tolkien", BirthDate = new DateTime(1892, 1, 3) }
            };
            modelBuilder.Entity<AuthorEntity>().HasData(authors);
            //----------------------------------------------------------------
            var genres = new List<GenreEntity>
            {
                new GenreEntity {Id = 1, Name = "Fantasy" },
                new GenreEntity {Id = 2, Name = "Adventure" },
                new GenreEntity {Id = 3, Name = "Drama" }
            };
            modelBuilder.Entity<GenreEntity>().HasData(genres);
            //----------------------------------------------------------
            //var booksGenres = new List<BookEntityGenreEntity> 
            //{
            //    new BookEntityGenreEntity {GenreId = 1, BookId = 1},
            //    new BookEntityGenreEntity {GenreId = 3, BookId = 2},
            //    new BookEntityGenreEntity {GenreId = 2, BookId = 3},
            //};
            //modelBuilder.Entity<BookEntityGenreEntity>().HasData(booksGenres);

        }
    }
}
