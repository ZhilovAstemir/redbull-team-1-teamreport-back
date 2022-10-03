using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.WebAPI.Helpers;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task Invoke(HttpContext context, ITeamService teamService)
    {
        try
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, teamService, token);
        }
        catch
        {
            throw new TokenValidationException();
        }
        await _next(context);
    }

    public async Task AttachUserToContext(HttpContext context, ITeamService teamService, string token)
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
        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "user").Value);

        context.Items["Member"] = await teamService.Get(userId);
    }
}