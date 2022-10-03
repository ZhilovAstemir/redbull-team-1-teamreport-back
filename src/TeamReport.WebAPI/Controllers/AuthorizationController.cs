﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.Models.Requests;
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
    public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.GetUserForLogin(request.Email, request.Password);
        if(user == null)
        {
            return NotFound();
        }

        return Ok(await _authService.GetToken(user));
    }
    
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult Register([FromBody] MemberRegistrationRequest member)
    {
        var memberModel = _mapper.Map<MemberRegistrationRequest, MemberModel>(member);

        var id = _authService.Register(memberModel);
        
        return Ok(id);
    }

}
