using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers
{
    public class UsersController(ILogger<UsersController> logger, IUsersService usersService)
        : ControllerBase
    {

        private readonly ILogger<UsersController> _logger = logger;

        [HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            await Task.Delay(1000);
            return Ok(usersService.GetUsers());
        }

    }
}
