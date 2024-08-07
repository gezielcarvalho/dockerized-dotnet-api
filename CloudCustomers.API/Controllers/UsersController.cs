using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers
{
    [ApiController] // This attribute indicates that this class is a controller
    [Route("[controller]")] // This attribute maps routes to this controller
    public class UsersController(ILogger<UsersController> logger, IUsersService usersService)
        : ControllerBase
    {

        private readonly ILogger<UsersController> _logger = logger;

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await usersService.GetUsers();
            if (result is { Count: 0 })
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
