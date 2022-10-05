using FluentAssertions;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Tests.Models;

public class CompanyModelTest
{
    [Fact]
    public void ShouldBeAbleToCreateCompanyModel()
    {
        var model = new CompanyModel();
        model.Should().NotBeNull();
    }

    [Fact]
    public void ShouldMemberModelHaveProperties()
    {
        var model = new CompanyModel()
        {
            Id = 1,
            Name = "Name"
        };

        model.Id.Should().BeOfType(typeof(int));
        model.Name.Should().BeOfType<string>();
    }
}