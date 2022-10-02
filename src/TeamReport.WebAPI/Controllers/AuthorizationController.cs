using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Models.Requests;

namespace TeamReport.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : Controller
{
    private readonly IAuthorizationServices _authService;

    public AuthorizationController(IAuthorizationServices authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.GetUserForLogin(request.Email, request.Password);
        if(user == null)
        {
            return NotFound();
        }

        return Ok(await _authService.GetToken(user));
    }
}
