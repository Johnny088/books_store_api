using books_store_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Repositories
{
    public class BookRepository: GenericRepository<BookEntity>
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
            :base(context)
        {
            _context = context;
        }
        public IQueryable<BookEntity> Books => GetAll();

        public async Task<List<BookEntity>> getByYearAsync(int year)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.PublishedYear == year)
                .ToListAsync();
        }
        public async Task<List<BookEntity>> getByRatingAsync(int rating)
        {
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Rating == rating)
                .ToListAsync();
        }

        public async Task<List<BookEntity>> GetByGenreAsync(string genre)
        {

            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Genres)
                .Where(b => b.Genres.Any(g => g.Name.ToLower() == genre.ToLower()))
                .ToListAsync();
        }
        public async Task<List<BookEntity>> GetByAuthorAsync(string author)
        {

            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Where(b => b.Author.Name.ToLower() == author.ToLower())
                .ToListAsync();
        }


    }
}
