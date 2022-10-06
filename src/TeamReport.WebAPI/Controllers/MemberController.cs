﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/members")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public MemberController(IMemberService memberService, IMapper mapper, IEmailService emailService)
    {

        _memberService = memberService;
        _mapper = mapper;
        _emailService = emailService;   
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _memberService.Login(request.Email, request.Password);
            return Ok(await _memberService.GetToken(user));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register([FromBody] MemberRegistrationRequest member)
    {
        try
        {
            var memberModel = _mapper.Map<MemberRegistrationRequest, MemberModel>(member);

            var id = await _memberService.Register(memberModel);

            return Ok(await _memberService.GetToken(memberModel));
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost("invite")]
    public async Task<IActionResult> InviteMember([FromBody] InviteMemberModelRequest member)
    {
        var path = this.GetRequestPath();
        var request = _mapper.Map<InviteMemberModelRequest, InviteMemberRequest>(member);
        _emailService.InviteMember(request, path);
        return Ok();
    }
}
