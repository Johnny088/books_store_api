using books_store_DAL;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_store_api.Controllers
{
    public class GenreController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GenreRepository _genreRepository;

        public GenreController(AppDbContext context, GenreRepository genreRepository)
        {
            _context = context;
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result =  await _genreRepository.Genres.ToListAsync();
            return Ok(result);
        }
    }
}
