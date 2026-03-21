using books_store_BLL.Dtos.Services;
using Microsoft.AspNetCore.Mvc;

namespace books_store_api.Controllers.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult GetAction(this ControllerBase controller, ServiceResponse response)
        {
            return response.Success ? controller.Ok(response) : controller.BadRequest(response);
        }
    }
}
