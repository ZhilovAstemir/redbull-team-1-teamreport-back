using FluentAssertions;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Tests.Models;

public class MemberModelTest
{
    [Fact]
    public void ShouldBeAbleToCreateMemberModel()
    {
        var model = new MemberModel();
        model.Should().NotBeNull();
    }

    [Fact]
    public void ShouldMemberModelHaveProperties()
    {
        var model = new MemberModel()
        {
            Company = new CompanyModel(),
            Email="email@email.email",
            FirstName = "FirstName",
            Id=1,
            LastName = "LastName",
            Password = "Password",
            Title = "Title"
        };

        model.Id.Should().BeOfType(typeof(int));
        model.Email.Should().BeOfType<string>();
        model.FirstName.Should().BeOfType<string>();
        model.LastName.Should().BeOfType<string>();
        model.Password.Should().BeOfType<string>();
        model.Title.Should().BeOfType<string>();
        model.Company.Should().BeOfType<CompanyModel>();
    }
}