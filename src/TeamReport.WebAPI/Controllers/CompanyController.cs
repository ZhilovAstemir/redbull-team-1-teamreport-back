using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Helpers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Controllers;


[Route("api/company")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;
    private readonly IMapper _mapper;
    public CompanyController(ICompanyService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    [Route("name")]
    public async Task<IActionResult> GetCompanyName()
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ?? throw new Exception();
            var company = await _service.GetCompany(member.Id);
            return Ok(company);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterCompany([FromBody] CompanyRegistrationRequest request)
    {
        try
        {
            var memberModel = _mapper.Map<CompanyRegistrationRequest, MemberModel>(request);

            var registeredMemberWithCompany = await _service.Register(memberModel);
            if (registeredMemberWithCompany != null)
            {
                return Ok(registeredMemberWithCompany);
            }

            return UnprocessableEntity(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("update")]
    public async Task<IActionResult> UpdateCompanyName([FromBody] string newCompanyName)
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ?? throw new Exception();
            var updated = await _service.SetName(member.Id, newCompanyName);

            if (updated != null)
            {
                return Ok(updated);
            }

            return UnprocessableEntity(newCompanyName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
