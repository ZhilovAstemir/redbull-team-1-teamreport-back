using FluentAssertions;
using TeamReport.Data.Entities;

namespace TeamReport.Data.Tests.Entities;

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
        var week = new Week()
        {
            Id = 1,
            DateStart = DateTime.MaxValue,
            DateEnd = DateTime.MaxValue
        };

        week.Id.Should().Be(1);
        week.DateStart.Should().Be(DateTime.MaxValue);
        week.DateEnd.Should().Be(DateTime.MaxValue);
    }
}