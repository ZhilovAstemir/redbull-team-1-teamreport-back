using FluentAssertions;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class CompanyServiceTest
{
    private readonly ServiceTestFixture _fixture;

    public CompanyServiceTest()
    {
        _fixture = new ServiceTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateCompanyService()
    {
        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());
        service.Should().NotBeNull().And.BeOfType<CompanyService>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetCompany()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        context.Members.Add(member);
        await context.SaveChangesAsync();

        var company = await service.GetCompany(member.Id);
        company.Should().NotBeNull();
        company.Name.Should().Be(member.Company.Name);
    }

    [Fact]
    public async Task ShouldGetCompanyReturnNullIfMemberHaveNoCompany()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        member.Company = null;
        context.Members.Add(member);
        await context.SaveChangesAsync();

        var company = await service.GetCompany(member.Id);
        company.Should().BeNull();
    }

    [Fact]
    public async Task ShouldSetCompanyName()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        context.Members.Add(member);
        await context.SaveChangesAsync();

        var company = await service.GetCompany(member.Id);
        company.Should().NotBeNull();
        company.Name.Should().Be(member.Company.Name);

        var newCompanyName = "New Company Name";
        var updatedCompany = await service.SetName(member.Id, newCompanyName);

        updatedCompany.Should().NotBeNull();
        updatedCompany.Name.Should().Be(newCompanyName);
    }

    [Fact]
    public async Task ShouldSetCompanyNameReturnNullIfCanNotUpdateCompanyUsingCompanyRepository()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var memberRepositoryMock = new Mock<IMemberRepository>();
        memberRepositoryMock.Setup(x => x.Update(It.IsAny<Member>())).Returns(Task.FromResult(false));

        var service = new CompanyService(new CompanyRepository(context),
            memberRepositoryMock.Object, _fixture.GetMapper());

        var member = _fixture.GetMember();
        context.Members.Add(member);
        await context.SaveChangesAsync();

        var newCompanyName = "New Company Name";
        var updatedCompany = await service.SetName(member.Id, newCompanyName);

        updatedCompany.Should().BeNull();
    }

    [Fact]
    public async Task ShouldSetCompanyNameReturnNullIfCanNotGetCompanyUsingService()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        member.Company = null;

        var newCompanyName = "New Company Name";
        var updatedCompany = await service.SetName(member.Id, newCompanyName);

        updatedCompany.Should().BeNull();
    }

    [Fact]
    public async Task ShouldRegisterCompany()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        var createdMember = await service.Register(memberModel);

        createdMember.Should().NotBeNull();
        createdMember.Company.Should().NotBeNull();

        context.Companies.Should().HaveCount(1);
        context.Members.Should().HaveCount(1);
    }

    [Fact]
    public async Task ShouldRegisterCompanyReturnNullIfMemberHaveNoCompany()
    {
        _fixture.ClearDatabase();

        var context = _fixture.GetContext();
        var service = new CompanyService(new CompanyRepository(context),
            new MemberRepository(context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Company = null;
        var createMember = await service.Register(memberModel);

        createMember.Should().BeNull();

        context.Companies.Should().BeEmpty();
        context.Members.Should().BeEmpty();
    }
}