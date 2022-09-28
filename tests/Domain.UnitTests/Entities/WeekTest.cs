using FluentAssertions;
using redbull_team_1_teamreport_back.Domain.Entities;

namespace Domain.UnitTests.Entities;

public class WeekTest
{
    [Fact]
    public void ShouldBeAbleToCreateWeek()
    {
        var week = new Week();
        week.Should().NotBeNull();
    }

    [Fact]
    public void ShouldWeekHaveProperties()
    {
        var week = new Week();
        week.Id = 1;
        week.DateStart=DateTime.MaxValue;
        week.DateEnd=DateTime.MaxValue;
        
        week.Id.Should().Be(1);
        week.DateStart.Should().Be(DateTime.MaxValue);
        week.DateEnd.Should().Be(DateTime.MaxValue);
    }
}