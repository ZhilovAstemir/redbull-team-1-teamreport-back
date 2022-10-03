using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.Models.Requests;

namespace TeamReport.WebAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/auth")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationServices _authService;
    private readonly IMapper _mapper;

    public AuthorizationController(IAuthorizationServices authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
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
