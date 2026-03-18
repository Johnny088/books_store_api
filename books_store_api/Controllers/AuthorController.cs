using books_store_DAL;
using books_store_DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace books_store_api.Controllers
{
    [ApiController]
    [Route("api/author")]  //our controller path
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthorController(AppDbContext context)
        {
            
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return Ok(authors);
        }
        [HttpPost]
        public async Task<IActionResult> Createtsync([FromBody]AuthorEntity entity)
        {
            await _context.Authors.AddAsync(entity);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                return BadRequest("something went wrong!");
            }
            return Ok("Author has added successfuly");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            return Ok("put");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            return Ok("delete");
        }

    }
}
