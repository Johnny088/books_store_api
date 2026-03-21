using books_store_api.Controllers.Extensions;
using books_store_BLL.Dtos.Book;
using books_store_BLL.Dtos.Services;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_store_api.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;
        public readonly BookService _bookService;

        public BookController(BookRepository bookRepository, BookService bookService)
        {
            _bookRepository = bookRepository;
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _bookService.GetAllAsync();
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateBookDto dto)
        {
            var response = await _bookService.CreateAsync(dto);
            return this.GetAction(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateBookDto dto)
        {
            var response = await _bookService.UpdateAsync(dto);
            return this.GetAction(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery]int id)
        {
            var response = await _bookService.DeleteAsync(id);
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
