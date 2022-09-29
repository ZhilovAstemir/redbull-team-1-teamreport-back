using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.Models.Requests;

namespace TeamReport.WebAPI.Controllers;
[ApiController]
[Authorize]
[Produces("application/json")]
[Route("[controller]")]
public class MemberController : Controller
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
    public ActionResult AddClient([FromBody] MemberRegistrationRequest member)
    {
        var id = _memberService.AddMember(_mapper.Map<MemberModel>(member));
        return Created($"{this.GetRequestPath()}/{id}", id);
    }
}
