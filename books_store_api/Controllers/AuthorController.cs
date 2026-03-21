using books_store_api.Controllers.Extensions;
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
            
            var response = await _authorService.GetAllAsync();
            return this.GetAction(response);
            
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var response = await _authorService.GetByIdAsync(id);
            return this.GetAction(response);

        }

        [HttpPost]
        public async Task<IActionResult> Createtsync([FromBody]CreateAuthorDto dto)
        {
            var response = await _authorService.CreateAsync(dto);

            return this.GetAction(response);

        }

        [HttpPut]
        
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAuthorDto dto)
        {
           
            var response = await _authorService.UpdateAsync(dto);
            return this.GetAction(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            
            var response = await _authorService.DeleteAsync(id);
            return this.GetAction(response);
        }

    }
}
