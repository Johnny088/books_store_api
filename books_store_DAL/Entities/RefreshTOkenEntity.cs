using books_store_DAL.Entities.identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public class RefreshTokenEntity : BaseEntity
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresData { get; set; }
        public bool IsExpired => DateTime.UtcNow > ExpiresData;
        public bool IsUsed { get; set; } = false;
        public string UserId { get; set; } = string.Empty;
        public AppUserEntity? User { get; set; }
    }
}
