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
    public async Task<string> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.GetUserForLogin(request.Email, request.Password);

        return await _authService.GetToken(user);
    }
}
