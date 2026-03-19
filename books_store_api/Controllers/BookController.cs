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

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var books = await _bookRepository.Books.ToListAsync();
            return Ok(books);
        }

        [HttpGet("Year")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var books = await _bookRepository.getByYearAsync(year);
            return Ok(books);
        }

        [HttpGet("Rating")]
        public async Task<IActionResult> GetByRating(int rating)
        {
            var books = await _bookRepository.getByRatingAsync(rating);
            return Ok(books);
        }
    }
}
