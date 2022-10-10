using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;

namespace TeamReport.Data.Tests.Repositories;
public class WeekRepositoryTests
{
    private readonly RepositoryTestFixture _fixture;
    private readonly ApplicationDbContext _context;

    public WeekRepositoryTests()
    {
        _fixture = new RepositoryTestFixture();
        _context = _fixture.GetContext();
    }

    [Fact]
    public void ShouldBeAbleToCreateWeekRepository()
    {
        var repository = new WeekRepository(_context);
        repository.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToAddWeek()
    {
        _fixture.ClearDatabase(_context);
        var repository = new WeekRepository(_context);
        var week = new Week()
        {
            DateEnd = new DateTime(2020, 10, 10),
            DateStart = new DateTime(2020, 10, 03)
        };
        
        repository.Add(week);

        _context.Weeks.Should().Contain(week);
    }

    [Fact]
    public void ShouldBeAbleToGetWeekByEndDate()
    {
        _fixture.ClearDatabase(_context);
        var repository = new WeekRepository(_context);
        var actualWeek = new Week()
        {
            DateEnd = new DateTime(2022, 10, 10),
            DateStart = new DateTime(2022, 10, 03)
        };
        repository.Add(actualWeek);
        var newWeek = repository.GetWeekByEndDate(actualWeek.DateEnd);
        Week expectedWeek = newWeek.Result;

        Assert.Equal(expectedWeek, actualWeek);
    }
}
