using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string? SecretKey { get; set; }
        public int ExpHours { get; set; } = 1;
    }
}
