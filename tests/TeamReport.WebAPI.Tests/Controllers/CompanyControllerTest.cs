using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;

namespace TeamReport.WebAPI.Tests.Controllers;

public class CompanyControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly ICompanyService _companyService;

    public CompanyControllerTest()
    {
        _fixture = new ControllerTestFixture();
        var context = _fixture.GetContext();
        _companyService = new CompanyService(new CompanyRepository(context), new MemberRepository(context), _fixture.GetMapperMock());
    }

    [Fact]
    public void ShouldBeAbleToCreateCompanyController()
    {
        var controller = new CompanyController(_companyService, _fixture.GetMapperMock());
        controller.Should().NotBeNull().And.BeOfType<CompanyController>();
    }

    [Fact]
    public async Task ShouldCompanyControllerRegisterCompanyWithFirstMember()
    {
        var controller = new CompanyController(_companyService, _fixture.GetMapperMock());
        controller.Should().NotBeNull().And.BeOfType<CompanyController>();

        var request = _fixture.GetCompanyRegistrationRequest();
        var response = await controller.RegisterCompany(request);

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<Member>();
    }

    [Fact]
    public async Task ShouldCompanyControllerGetCompanyName()
    {
        var controller = new CompanyController(_companyService, _fixture.GetMapperMock());
        controller.Should().NotBeNull().And.BeOfType<CompanyController>();

        var request = _fixture.GetCompanyRegistrationRequest();
        var response = await controller.RegisterCompany(request);

        var member = (response as OkObjectResult)?.Value as Member;
        member.Should().NotBeNull();

        var nameResponse = await controller.GetCompanyName();
        nameResponse.Should().BeOfType<OkObjectResult>();

    }

}