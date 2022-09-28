using FluentAssertions;
using redbull_team_1_teamreport_back.Domain.Entities;

namespace Domain.UnitTests.Entities;

public class MemberTest
{
    [Fact]
    public void ShouldBeAbleToCreateMember()
    {
        var member = new Member();
        member.Should().NotBeNull();
    }

    [Fact]
    public void ShouldMemberHaveProperties()
    {
        var member = new Member();

        member.Id = 1;
        var company = new Company();
        member.Company = company;
        member.FirstName = "FirstName";
        member.LastName = "LastName";
        member.Email = "email@email.com";
        member.Title = "Title";

        member.Id.Should().Be(1);
        member.Company.Should().Be(company);
        member.FirstName.Should().Be("FirstName");
        member.LastName.Should().Be("LastName");
        member.Email.Should().Be("email@email.com");
        member.Title.Should().Be("Title");
    }
}