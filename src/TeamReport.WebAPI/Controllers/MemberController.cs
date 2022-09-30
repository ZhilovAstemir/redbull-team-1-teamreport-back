using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Services.Interfaces;

namespace TeamReport.WebAPI.Controllers;

[ApiController]
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

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_memberService.GetAllMembers());
    }
}
