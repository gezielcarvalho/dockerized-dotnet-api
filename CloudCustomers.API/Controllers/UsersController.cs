using CloudCustomers.API.Models;
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
            var result = await usersService.GetUsers();
            if (result is { Count: 0 })
            {
                return NotFound();
            }
            return Ok(await usersService.GetUsers());
        }

    }
}
