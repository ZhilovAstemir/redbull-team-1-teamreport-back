using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.WebAPI.Controllers;

[ApiController]
[Authorize]
[Produces("application/json")]
[Route("api/members")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;

    public MemberController(IMemberService memberService, IMapper mapper)
    {
        _memberService = memberService;
        _mapper = mapper;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok( await _memberService.GetAll());
    }
}
