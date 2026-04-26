using books_store_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshTokenEntity>
    {
        public RefreshTokenRepository(AppDbContext context) 
            : base(context)
        {
            
        }

        public IQueryable<RefreshTokenEntity> RefreshTokens => GetAll();
        public async Task<RefreshTokenEntity?> GetByTokenAsync(string token)
        {
           return await RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }
    }
}
