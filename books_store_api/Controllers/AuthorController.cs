using books_store_BLL.Dtos.Author;
using books_store_BLL.Dtos.Services;
using books_store_DAL;
using books_store_DAL.Entities;
using books_store_DAL.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_store_api.Controllers
{
    [ApiController]
    [Route("api/author")]  //our controller path
    public class AuthorController : ControllerBase
    {
        
        private readonly AuthorRepository _authorRepository;
        private readonly AuthorService _authorService;
        public AuthorController(AppDbContext context, AuthorRepository authorRepository, AuthorService authorService)
        {


            _authorRepository = authorRepository;
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (result == null)
            {
                return BadRequest("Invalid ID: author wasn't found");
            }
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> Createtsync([FromBody]CreateAuthorDto dto)
        {
            var result = await _authorService.CreateAsync(dto);
            if (result == null)
            {
                return BadRequest("couldn't add the author");
            }
            return Ok(result);
            
        }





        [HttpPut]
        
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAuthorDto dto)
        {
           
            var result = await _authorService.UpdateAsync(dto);
            if (result == null)
            {
                return BadRequest("couldn't update the author");
            }
            
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            
            var result = await _authorService.DeleteAsync(id);
            if (result == null)
            {
                return BadRequest("couldn't deleted author");
            }
            return Ok(result);
        }

    }
}
