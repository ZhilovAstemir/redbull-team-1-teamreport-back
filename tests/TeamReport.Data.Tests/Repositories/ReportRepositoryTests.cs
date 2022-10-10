using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Data.Entities;
using TeamReport.Data.Enums;
using TeamReport.Data.Persistence;

namespace TeamReport.Data.Tests.Repositories;
public class ReportRepositoryTests
{
    private readonly RepositoryTestFixture _fixture;
    private readonly ApplicationDbContext _context;

    public ReportRepositoryTests()
    {
        _fixture = new RepositoryTestFixture();
        _context = _fixture.GetContext();
    }
	
	public void ShouldBeAbleToCreateReportRepository()
    {
        var repository = new ReportRepository(_context);
        repository.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToGetReportsByMemberId()
    {
        _fixture.ClearDatabase(_context);
        var repository = new ReportRepository(_context);
        var member = new Member()
        {
            Id = 1,
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password@@@"
        };
        var week = new Week()
        {
            DateEnd = new DateTime(2020, 10, 10),
            DateStart = new DateTime(2021, 10, 10)
        };
        var report = new Report
        {
            Morale = Emotion.Good, 
            MoraleComment = "comment", 
            Stress = Emotion.Okay,
            StressComment = "comment",
            Workload = Emotion.Good,
            WorkloadComment = "comment",
            High = "okay",
            Low = "okay", 
            Else = "okay", 
            Member = member,
            Week = week
        };
       repository.Create(report, week, member);

       List<Report> expectedReports = await repository.GetMemberReportsById(report.Member.Id);

       Assert.NotNull(expectedReports);
       expectedReports.Should().Contain(report);
	}

    [Fact]
    public void ShouldBeAbleToAddReport()
    {
        _fixture.ClearDatabase(_context);
        var repository = new ReportRepository(_context);
        var week = new Week()
        {
            DateEnd = new DateTime(2020, 10, 10),
            DateStart = new DateTime(2020, 10, 03)
        };
        var member = new Member()
        {
            FirstName = "Alex",
            LastName = "Mister",
            Title = "1",
            Email = "alex@gmail.com",
            Password = "azino777"
        };
        var report = new Report()
        {
            Morale = Emotion.Good,
            MoraleComment = "Good",
            Stress = Emotion.Okay,
            StressComment = "Nice",
            Workload = Emotion.Good,
            WorkloadComment = "Okay",
            High = "Good",
            Low = "Good",
            Else = "Nice",
        };

        repository.Create(report, week, member);

        _context.Reports.Should().Contain(report);
    }
}
