using books_store_DAL.Entities;
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
    }
}
