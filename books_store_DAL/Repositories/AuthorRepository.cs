using books_store_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Repositories
{
    public class AuthorRepository: GenericRepository<AuthorEntity>
    {
        private readonly AppDbContext _context;
        public AuthorRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
        }
        public IQueryable<AuthorEntity> Authors => GetAll();
        public async Task<AuthorEntity?> GetByNameAsync(string name)
        {
            return await Authors
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<List<BookEntity>> GetBooksAsync(AuthorEntity entity)
        {
       
            return await _context.Books
                .AsNoTracking()
                .Where(b => b.Id == entity.Id)
                .ToListAsync();
        }
        public async Task<bool> AddBookAsync(AuthorEntity author, BookEntity book)
        {
            var authorBooks = await GetBooksAsync(author);
            if (!authorBooks.Contains(book))
            {
                authorBooks.Add(book);
                return(await _context.SaveChangesAsync()) != 0;
            }
            return false;
        }
    }
}
