using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Data.Entities;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Helpers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/team")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;

    public TeamController(ITeamService teamService, IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllTeamMembers()
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ??
                         throw new EntityNotFoundException("Authorized member should have data in HttpContext");
            if (member.Company == null)
                throw new EntityNotFoundException("Company not found");

            var teamMembers = await _teamService.GetAllTeamMembers(member.Company.Id);

            return Ok(teamMembers);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

    [HttpGet]
    [Route("reporters")]
    public async Task<IActionResult> GetMemberReporters()
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ??
                         throw new EntityNotFoundException("Authorized member should have data in HttpContext");

            var reporters = await _teamService.GetMemberReporters(member.Id);

            return Ok(reporters);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

    [HttpGet]
    [Route("leaders")]
    public async Task<IActionResult> GetMemberLeaders()
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ??
                         throw new EntityNotFoundException("Authorized member should have data in HttpContext");

            var leaders = await _teamService.GetMemberLeaders(member.Id);

            return Ok(leaders);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

    [HttpPost]
    [Route("leaders")]
    public async Task<IActionResult> UpdateMemberLeaders([FromBody] MemberIdsListRequest request)
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ??
                         throw new EntityNotFoundException("Authorized member should have data in HttpContext");

            var leaders = await _teamService.UpdateMemberLeaders(member.Id, request.MembersIds);

            return Ok(leaders);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

    [HttpPost]
    [Route("reporters")]
    public async Task<IActionResult> UpdateMemberReporters([FromBody] MemberIdsListRequest request)
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ??
                         throw new EntityNotFoundException("Authorized member should have data in HttpContext");

            var reporters = await _teamService.UpdateMemberReporters(member.Id, request.MembersIds);

            return Ok(reporters);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

}
