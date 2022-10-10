using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Tests.Controllers;
public class ReportControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IReportService _service;

    public ReportControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _service = new ReportService(new ReportRepository(
            _fixture.GetContext()),
            _fixture.GetMapper(),
            new WeekRepository(_fixture.GetContext()));
    }

    [Fact]
    public void ShouldBeAbleToCreateReportController()
    {
        var controller = new ReportController(_service, _fixture.GetMapper());
        controller.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToAddReport()
    {
        _fixture.ClearDatabase();
        var controller = new ReportController(_service, _fixture.GetMapper());
        var reportRequest = _fixture.GetReportRequest();

        var reportId = controller.AddReport(reportRequest);

        Assert.NotNull(reportId);
    }

    [Fact]
    public void ShouldBeAbleToGetReportsByMemberId()
    {
        _fixture.ClearDatabase();
        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var reportController = new ReportController(_service, _fixture.GetMapper());
        var actualReport = _fixture.GetReportModel();
        var member = _fixture.GetMemberWithId();
        _service.Add(actualReport, member);
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        reportController.ControllerContext = controllerContext;

        var reports = reportController.GetReportsByMemberId();
        var result = reports.Result;

        (result.Result as OkObjectResult)?.Value.Should().BeOfType<List<ReportResponse>>();
    }
}
