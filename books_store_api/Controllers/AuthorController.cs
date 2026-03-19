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
        public AuthorController(AppDbContext context, AuthorRepository authorRepository)
        {

            
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var authors = await _authorRepository.Authors.ToListAsync();
            return Ok(authors);
        }
        [HttpPost]
        public async Task<IActionResult> Createtsync([FromBody]AuthorEntity entity)
        {
            
            bool result = await _authorRepository.CreateAsync(entity);
            if (!result)
            {
                return BadRequest("couldn't add the author");
            }
            return Ok("Author has added successfuly");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] AuthorEntity entity)
        {
            bool result = await _authorRepository.UpdateAsync(entity);
            if (!result)
            {
                return BadRequest("couldn't update the author");
            }
            return Ok("Author has updated successfuly");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            bool result = await _authorRepository.DeleteAsync(id);
            if (!result)
            {
                return BadRequest("couldn't deleted author");
            }
            return Ok("Author has deleted successfuly");
        }

    }
}
