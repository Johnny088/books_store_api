using books_store_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshTOkenEntity>
    {
        public RefreshTokenRepository(AppDbContext context) 
            : base(context)
        {
            
        }

        public IQueryable<RefreshTOkenEntity> RefreshTokens => GetAll();
    }
}
