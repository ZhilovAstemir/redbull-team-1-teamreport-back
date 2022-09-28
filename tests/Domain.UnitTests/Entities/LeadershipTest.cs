using FluentAssertions;
using redbull_team_1_teamreport_back.Domain.Entities;

namespace Domain.UnitTests.Entities;

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
        var leadership = new Leadership();

        leadership.Id = 1;
        var leader = new Member();
        var member = new Member();
        leadership.Leader = leader;
        leadership.Member = member;

        leadership.Id.Should().Be(1);
        leadership.Leader.Should().Be(leader);
        leadership.Member.Should().Be(member);
    }
}