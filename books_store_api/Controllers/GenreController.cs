using books_store_api.Controllers.Extensions;
using books_store_BLL.Dtos.Genre;
using books_store_BLL.Dtos.Pagination;
using books_store_BLL.Dtos.Services;
using books_store_DAL;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace books_store_api.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Genre")]
    public class GenreController : ControllerBase
    {
        
        
        private readonly GenreService _genreService;
        private readonly ILogger<GenreController> _logger;

        public GenreController(GenreService genreService, ILogger<GenreController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]PaginationDto pagination)
        {
            _logger.LogInformation(2000, $"{DateTime.Now} Get all genres request");
           

            var result = await _genreService.GetAllAsync(pagination);
            return this.GetAction(result);
        }
     
        [HttpGet("name")]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
        {
            var result = await _genreService.GetByName(name);
            return this.GetAction(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
        {
            var result = await _genreService.GetByIdAsync(id);
            return this.GetAction(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateGenreDto dto)
        {
            var result = await _genreService.CreateAsync(dto);
            return this.GetAction(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateGenreDto dto)
        {
            var result = await _genreService.UpdateAsync(dto);
            return this.GetAction(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            var result = await _genreService.DeleteAsync(id);
            return this.GetAction(result);
        }

    }
    
        
}
