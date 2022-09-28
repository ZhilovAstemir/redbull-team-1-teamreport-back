using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using redbull_team_1_teamreport_back.Domain.Identity;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserLoginController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<UserLoginController> _logger;
    public UserLoginController(ILogger<UserLoginController> logger, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(_userManager.Users);
    }
}
