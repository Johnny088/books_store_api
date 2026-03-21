using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Services
{
    public class ServiceResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
        public object? Payload { get; set; } = null;
    }
}
