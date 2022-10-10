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

    [Fact]
    public void ShouldBeAbleToCreateReportRepository()
    {
        var repository = new ReportRepository(_context);
        repository.Should().NotBeNull();
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
