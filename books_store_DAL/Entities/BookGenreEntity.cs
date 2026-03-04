using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public class BookGenreEntity
    {
        public int BookId { get; set; }
        public BookEntity? Book { get; set; } = null!;
        public int GenreId { get; set; }
        public GenreEntity? Genre { get; set; } = null!;
    }
}
