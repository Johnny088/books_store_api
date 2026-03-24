using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public class AuthorEntity: BaseEntity
    {
        
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Image { get; set; }
        public string? Country { get; set; }
        public IEnumerable<BookEntity> Books {  get; set; } = new List<BookEntity>();
    }
}
