using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Data.Entities;
using TeamReport.Domain.Exceptions;
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
            var member = (Member)HttpContext.Items["Member"] ?? throw new EntityNotFoundException("Authorized member should have data in HttpContext");
            var company = await _service.GetCompany(member.Id);
            return Ok(company);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
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

            return BadRequest(request);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }

    [HttpPut]
    [Authorize]
    [Route("update")]
    public async Task<IActionResult> UpdateCompanyName([FromBody] UpdateCompanyNameRequest request)
    {
        try
        {
            var member = (Member)HttpContext.Items["Member"] ?? throw new EntityNotFoundException("Authorized member should have data in HttpContext");
            var updated = await _service.SetName(member.Id, request.NewCompanyName);

            if (updated != null)
            {
                return Ok(updated);
            }

            return BadRequest(request);
        }
        catch
        {
            return BadRequest("Something went wrong during processing your request. Please try again later.");
        }
    }
}
