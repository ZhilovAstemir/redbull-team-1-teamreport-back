using FluentAssertions;
using TeamReport.Data.Entities;
using TeamReport.Domain.Exceptions;
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

    [Fact]
    public async void ShouldBeAbleToGetReportsByMemberId()
    {
        var service = new ReportService(_fixture.GetReportRepositoryMock().Object, _fixture.GetMapper(), _fixture.GetWeekRepositoryMock().Object);
        var actualReport = _fixture.GetReportModel();
        var member = _fixture.GetMemberWithId();

        var reportId = service.Add(actualReport, member);
        var expectedReports = await service.GetReportsByMemberId(member);

        reportId.Should().NotBeNull();
        expectedReports.Should().NotBeNull();
        expectedReports.Should().HaveCount(1);
        Assert.Equal(expectedReports[0].MoraleComment, actualReport.MoraleComment);
        Assert.Equal(expectedReports[0].StressComment, actualReport.StressComment);
        Assert.Equal(expectedReports[0].WorkloadComment, actualReport.WorkloadComment);
    }

    [Fact]
    public async void ThenGetReportsByMemberIdMemberIdIsNull_ShouldThrowDataException()
    {
        var service = new ReportService(_fixture.GetReportRepositoryMock().Object, _fixture.GetMapper(), _fixture.GetWeekRepositoryMock().Object);
        var actualReport = _fixture.GetReportModel();
        Member member = null;

        var expectedReports = async () => await service.GetReportsByMemberId(member);

        await expectedReports.Should().ThrowAsync<DataException>();
    }
}
