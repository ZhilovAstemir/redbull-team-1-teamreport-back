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
    public string Login([FromBody] LoginRequest request)
    {
        var user = _authService.GetUserForLogin(request.Email, request.Password);

        return _authService.GetToken(user);
    }
}
