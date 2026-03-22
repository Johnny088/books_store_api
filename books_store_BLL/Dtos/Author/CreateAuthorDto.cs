using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace books_store_BLL.Dtos.Author
{
    public class CreateAuthorDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
        public IFormFile? Image {  get; set; }
    }
}
