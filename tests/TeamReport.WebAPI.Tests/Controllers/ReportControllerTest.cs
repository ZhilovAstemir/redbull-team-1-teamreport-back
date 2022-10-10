using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;

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
}
