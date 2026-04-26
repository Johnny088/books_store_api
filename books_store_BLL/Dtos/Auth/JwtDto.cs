using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Auth
{
    public class JwtDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
