using FluentAssertions;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;
public class ReportServiceTest
{
    private readonly ServiceTestFixture _fixture;

    public ReportServiceTest()
    {
        _fixture = new ServiceTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateReportService()
    {
        var service = new ReportService(_fixture.GetReportRepositoryMock().Object, _fixture.GetMapper(), _fixture.GetWeekRepositoryMock().Object);
        service.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToAddReport()
    {
        var service = new ReportService(_fixture.GetReportRepositoryMock().Object, _fixture.GetMapper(), _fixture.GetWeekRepositoryMock().Object);
        var member = _fixture.GetMember();
        var report = _fixture.GetReportModel();

        var reportId = service.Add(report, member);
        int expectedId = reportId.Result;

        Assert.NotNull(expectedId);
    }
}
