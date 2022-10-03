using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Extensions;

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
        var user = await _memberService.Login(request.Email, request.Password);
        return Ok( await _memberService.GetToken(user));
    }
    
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register([FromBody] MemberRegistrationRequest member)
    {
        var memberModel = _mapper.Map<MemberRegistrationRequest, MemberModel>(member);

        var id = await _memberService.Register(memberModel);
        
        return Ok( await _memberService.GetToken(memberModel));
    }

    [HttpPost("invite")]
    public async Task<IActionResult> InviteMember([FromBody] InviteMemberRequest request)
    {
        var path = this.GetRequestPath();
        _emailService.InviteMember(request, path);
        return Ok();
    }
}
