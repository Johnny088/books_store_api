using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public class BookEntityGenreEntity
    {
        public int BookId { get; set; }
        public BookEntity BookEntity { get; set; } = null!;
        public int GenreId { get; set; }
        public GenreEntity GenreEntity { get; set; } = null!;
    }
}
