using System;
using System.Collections.Generic;
using System.Text;

namespace books_store_DAL.Entities
{
    public class BookEntityGenreEntity
    {
        public int BookEntityId { get; set; }
        public BookEntity BookEntity { get; set; } = null!;
        public int GenreEntityId { get; set; }
        public GenreEntity GenreEntity { get; set; } = null!;
    }
}
