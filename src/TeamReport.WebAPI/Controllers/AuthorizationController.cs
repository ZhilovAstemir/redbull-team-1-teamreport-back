using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TeamReport.Domain.Auth;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using IAuthorizationService = TeamReport.Domain.Services.Interfaces.IAuthorizationService;

namespace TeamReport.WebAPI.Controllers;


[ApiController]
[Route("api/auth")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authService;
    private readonly IMapper _mapper;

    public AuthorizationController(IAuthorizationService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("login")]
    public string Login([FromBody] LoginRequest request)
    {
        var user = _authService.Login(request.Email, request.Password);

        return _authService.GetToken(user);
    }
    
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult Register([FromBody] MemberRegistrationRequest member)
    {
        var memberModel = _mapper.Map<MemberRegistrationRequest, MemberModel>(member);

        var id = _authService.Register(memberModel);
        
        return Ok(_authService.GetToken(memberModel));
    }

    [HttpPost]
    [Route("token")]
    public IActionResult ValidateToken([FromBody] string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userEmail = jwtToken.Claims.First(x => x.Type == "user").Value;
            return Ok(userEmail);
        }
        catch
        {
            return Unauthorized();
        }
    }
}
