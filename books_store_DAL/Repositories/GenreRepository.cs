using books_store_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Repositories
{
    public class GenreRepository: GenericRepository<GenreEntity>
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _context = appDbContext;
        }
        public IQueryable<GenreEntity> Genres => GetAll();
        public async Task<GenreEntity?> GetByNameAsync(string name)
        {
            return await Genres.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
