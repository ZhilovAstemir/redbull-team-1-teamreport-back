using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamReport.Data.Entities;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Models;
using TeamReport.WebAPI.Extensions;

namespace TeamReport.WebAPI.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/reports")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;
    private readonly IMapper _mapper;

    public ReportController(IReportService reportService, IMapper mapper)
    {
        _reportService = reportService;
        _mapper = mapper;
    }

    [HttpGet("get-reports-by-member-id")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<List<ReportResponse>>> GetReportsByMemberId()
    {
        var member = this.HttpContext.Items["Member"];
        var reports = await _reportService.GetReportsByMemberId(member as Member);
        return Ok(_mapper.Map<List<ReportResponse>>(reports));
	}
	
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddReport([FromBody] ReportRequest report)
    {
        var member = this.HttpContext.Items["Member"];
        var id = await _reportService.Add(_mapper.Map<ReportModel>(report), member as Member);
        return Created($"{this.GetRequestPath()}/{id}", id);
    }
}
