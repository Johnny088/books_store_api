using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<BookEntity>? Books = new List<BookEntity>();
    }
}
