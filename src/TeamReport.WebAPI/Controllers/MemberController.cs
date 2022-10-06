using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Data.Entities;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Helpers;
using TeamReport.WebAPI.Models;

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
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }

    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register([FromBody] MemberRegistrationRequest request)
    {
        try
        {
            var memberModel = _mapper.Map<MemberRegistrationRequest, MemberModel>(request);
            memberModel.Company = new CompanyModel() { Id = request.CompanyId };

            var createdMemberModel = await _memberService.Register(memberModel);

            return Ok(await _memberService.GetToken(createdMemberModel));
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

    [HttpGet]
    [Route("info")]
    [Authorize]
    public async Task<IActionResult> GetMemberInformation()
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ?? throw new EntityNotFoundException("Authorized member should have data in HttpContext");
            var memberModel = _mapper.Map<Member, MemberModel>(member);

            return Ok(memberModel);

        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }


    [HttpPost]
    [Route("continue-registration")]
    [Authorize]
    public async Task<IActionResult> ContinueRegistration([FromBody] ContinueRegistrationRequest request)
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ?? throw new EntityNotFoundException("Authorized member should have data in HttpContext");
            var memberModel = _mapper.Map<Member, MemberModel>(member);
            memberModel.Title = request.Title;
            memberModel.Password = request.Password;

            var updatedMember = await _memberService.ContinueRegistration(memberModel);

            return Ok(await _memberService.GetToken(updatedMember));

        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }
}
