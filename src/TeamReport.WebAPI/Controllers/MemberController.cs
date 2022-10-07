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
    private readonly IEmailService _emailService;
    private readonly ITeamService _teamService;
    private readonly IConfiguration _configuration;

    public MemberController(IMemberService memberService, IMapper mapper,
        IEmailService emailService, ITeamService teamService, IConfiguration configuration)
    {

        _memberService = memberService;
        _mapper = mapper;
        _emailService = emailService;
        _teamService = teamService;
        _configuration = configuration;
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
        catch
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

    [HttpPost("invite")]
    [Authorize]
    public async Task<IActionResult> InviteMember([FromBody] InviteMemberModelRequest inviteMemberModelRequest)
    {
        try
        {
            var leader = (Member)HttpContext.Items["Member"] ??
                         throw new EntityNotFoundException("Authorized member should have data in HttpContext");

            var memberModel = _mapper.Map<InviteMemberModelRequest, MemberModel>(inviteMemberModelRequest);
            memberModel.Company = _mapper.Map<Company, CompanyModel>(leader.Company);

            var registeredModel = await _memberService.GetMemberByEmail(memberModel.Email);
            if (registeredModel == null)
            {
                registeredModel = await _memberService.Register(memberModel);
            }
            else
            {
                if (registeredModel.Password is null)
                {
                    registeredModel.Company = memberModel.Company;
                    registeredModel.FirstName = memberModel.FirstName;
                    registeredModel.LastName = memberModel.LastName;
                    await _memberService.UpdateMemberInformation(registeredModel);
                    registeredModel = await _memberService.GetMemberByEmail(registeredModel.Email);
                }
                else
                {
                    throw new MemberAlreadyRegisteredException(
                        $"Member with email {registeredModel.Email} is already registered");
                }
            }

            await _teamService.UpdateMemberLeaders(registeredModel.Id, new List<int>() { leader.Id });

            var domain = _configuration.GetValue<string>("Domain");

            _emailService.InviteMember(registeredModel, domain);
            return Ok(registeredModel);
        }
        catch (MemberAlreadyRegisteredException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }
}
