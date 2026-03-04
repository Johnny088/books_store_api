using Microsoft.AspNetCore.Mvc;

namespace books_store_api.Controllers
{
    [ApiController]
    [Route("api/author")]  //our controller path
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok("get");
        }
        [HttpPost]
        public async Task<IActionResult> Createtsync()
        {
            return Ok("post");
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
