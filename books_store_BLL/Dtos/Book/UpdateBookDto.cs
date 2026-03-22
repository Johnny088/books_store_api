using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace books_store_BLL.Dtos.Book
{
    public class UpdateBookDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public float Rating { get; set; }
        public int Pages { get; set; }
        public int PublishedYear { get; set; }
    }
}
