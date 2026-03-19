using books_store_DAL;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_store_api.Controllers
{
    [ApiController]
    [Route("api/Genre")]
    public class GenreController : ControllerBase
    {
        
        private readonly GenreRepository _genreRepository;

        public GenreController(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _genreRepository.Genres.ToListAsync();
            return Ok(result);
        }
        [HttpGet("name")]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
        {
            var result = await _genreRepository.GetByNameAsync(name);
            return Ok(result);
        }
    }
    
        
}
