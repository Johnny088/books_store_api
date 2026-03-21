using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace books_store_BLL.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public float Rating { get; set; }
        public int Pages { get; set; }
        public int PublishedYear { get; set; }

    }
}
