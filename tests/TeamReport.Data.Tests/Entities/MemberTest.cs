using FluentAssertions;
using TeamReport.Data.Entities;

namespace TeamReport.Data.Tests.Entities;

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
        var company = new Company();

        var member = new Member()
        {
            Id = 1,
            Company = company,
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "email@email.com",
            Title = "Title"
        };

        member.Id.Should().Be(1);
        member.Company.Should().Be(company);
        member.FirstName.Should().Be("FirstName");
        member.LastName.Should().Be("LastName");
        member.Email.Should().Be("email@email.com");
        member.Title.Should().Be("Title");
    }
}