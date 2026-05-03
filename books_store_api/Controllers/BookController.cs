using books_store_api.Controllers.Extensions;
using books_store_api.Settings;
using books_store_BLL.Dtos.Book;
using books_store_BLL.Dtos.Pagination;
using books_store_BLL.Dtos.Services;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_store_api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;
        public readonly BookService _bookService;
        private readonly string _booksPath;

        public BookController(BookRepository bookRepository, BookService bookService, IWebHostEnvironment environment)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
            string rootPath = environment.ContentRootPath;
            _booksPath = Path.Combine(rootPath, StaticFilesSetting.StorageDir, StaticFilesSetting.BooksDir);
            
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDto pagination)
        {
            var response = await _bookService.GetAllAsync(pagination);
            return this.GetAction(response);
        } //+

        [HttpGet("Year")]
        public async Task<IActionResult> GetByYearAsync([FromQuery]int year)
        {
            var response = await _bookService.GetByYearAsync(year);
            return this.GetAction(response);
        }//+

        [HttpGet("Rating")]
        public async Task<IActionResult> GetByRatingAsync([FromQuery]int rating)
        {
            var response = await _bookService.GetByRatingAsync(rating);
            return this.GetAction(response);
        } //+

        [HttpGet("genres")]
        public async Task<IActionResult> GetByGenresAsync([FromQuery]string genre)
        {
            var response = await _bookService.GetByGenresAsync(genre);
            return this.GetAction(response);
        }//+
        [HttpGet("authors")]
        public async Task<IActionResult> GetByAuthor([FromQuery]string author)
        {
            var books = await _bookRepository.GetByAuthorAsync(author);
            return Ok(books);
        }//----------

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateBookDto dto)
        {
            var response = await _bookService.CreateAsync(dto, _booksPath);
            return this.GetAction(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm]UpdateBookDto dto)
        {
            var response = await _bookService.UpdateAsync(dto, _booksPath);
            return this.GetAction(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery]int id)
        {
            var response = await _bookService.DeleteAsync(id, _booksPath);
            return this.GetAction(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery]int id)
        {
            var response = await _bookService.GetByIdAsync(id);
            return this.GetAction(response);
        }
    }
}
