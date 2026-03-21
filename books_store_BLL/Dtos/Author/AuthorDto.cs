using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_BLL.Dtos.Author
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string  Name { get; set; } = string.Empty;
        public DateTime BirthDate {  get; set; }
        public string? Image { get; set; }
    }
}
