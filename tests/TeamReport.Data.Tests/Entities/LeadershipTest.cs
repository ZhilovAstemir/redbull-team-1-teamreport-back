using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Entities;

namespace TeamReport.Data.Tests.Entities;

public class LeadershipTest
{
    [Fact]
    public void ShouldBeAbleToCreateLeadership()
    {
        var leadership = new Leadership();
        leadership.Should().NotBeNull();
    }

    [Fact]
    public void ShouldLeadershipHaveProperties()
    {
        var leader = new Member();
        var member = new Member();

        var leadership = new Leadership()
        {
            Id = 1,
            Leader = leader,
            Member = member
        };
        
        leadership.Id.Should().Be(1);
        leadership.Leader.Should().Be(leader);
        leadership.Member.Should().Be(member);
    }
}