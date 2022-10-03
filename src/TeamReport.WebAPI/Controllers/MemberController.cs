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

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddClient([FromBody] MemberRegistrationRequest member)
    {
        var id = await _memberService.AddMember(_mapper.Map<MemberModel>(member));
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok( await _memberService.GetAll());
    }
}
