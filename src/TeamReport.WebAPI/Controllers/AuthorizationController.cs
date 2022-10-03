﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using IAuthorizationService = TeamReport.Domain.Services.Interfaces.IAuthorizationService;

namespace TeamReport.WebAPI.Controllers;

[AllowAnonymous]
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
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.Login(request.Email, request.Password);
        return Ok( await _authService.GetToken(user));
    }
    
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register([FromBody] MemberRegistrationRequest member)
    {
        var memberModel = _mapper.Map<MemberRegistrationRequest, MemberModel>(member);

        var id = await _authService.Register(memberModel);
        
        return Ok( await _authService.GetToken(memberModel));
    }

}
