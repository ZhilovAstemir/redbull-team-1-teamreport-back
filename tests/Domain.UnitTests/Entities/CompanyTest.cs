using FluentAssertions;
using redbull_team_1_teamreport_back.Domain.Entities;

namespace Domain.UnitTests.Entities;

public class CompanyTest
{
    [Fact]
    public void ShouldBeAbleToCreateMember()
    {
        var company = new Company();
        company.Should().NotBeNull();
    }

    [Fact]
    public void ShouldCompanyHaveProperties()
    {
        var company = new Company();

        company.Id = 1;
        company.Name = "Company";

        company.Id.Should().Be(1);
        company.Name.Should().Be("Company");
    }
}