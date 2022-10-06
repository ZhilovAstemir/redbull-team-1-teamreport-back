using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TeamReport.Data.Repositories;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Tests.Controllers;

public class CompanyControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly ICompanyService _companyService;

    public CompanyControllerTest()
    {
        _fixture = new ControllerTestFixture();
        var context = _fixture.GetContext();
        _companyService = new CompanyService(new CompanyRepository(context), new MemberRepository(context), _fixture.GetMapper());
    }

    [Fact]
    public void ShouldBeAbleToCreateCompanyController()
    {
        var controller = new CompanyController(_companyService, _fixture.GetMapper());
        controller.Should().NotBeNull().And.BeOfType<CompanyController>();
    }

    [Fact]
    public async Task ShouldCompanyControllerRegisterCompanyWithItsFirstMember()
    {
        var controller = new CompanyController(_companyService, _fixture.GetMapper());
        controller.Should().NotBeNull().And.BeOfType<CompanyController>();

        var request = _fixture.GetCompanyRegistrationRequest();
        var response = await controller.RegisterCompany(request);

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();
    }

    [Fact]
    public async Task ShouldCompanyControllerGetCompanyName()
    {
        _fixture.ClearDatabase();

        var controller = new CompanyController(_companyService, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;


        var request = _fixture.GetCompanyRegistrationRequest();
        var response = await controller.RegisterCompany(request);

        var memberFromResponse = (response as OkObjectResult)?.Value as MemberModel;
        memberFromResponse.Should().NotBeNull();

        var nameResponse = await controller.GetCompanyName();
        nameResponse.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<CompanyModel>();

    }

    [Fact]
    public async Task ShouldCompanyControllerUpdateCompanyName()
    {
        _fixture.ClearDatabase();

        var controller = new CompanyController(_companyService, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;


        var request = _fixture.GetUpdateCompanyNameRequest();
        var updateResponse = await controller.UpdateCompanyName(request);
        updateResponse.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<CompanyModel>();


        var getResponse = await controller.GetCompanyName();
        getResponse.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<CompanyModel>();
    }


    [Fact]
    public async Task ShouldUpdateCompanyNameReturnBadRequestIfServiceCanNotUpdate()
    {
        _fixture.ClearDatabase();

        var companyServiceMock = new Mock<ICompanyService>();
        companyServiceMock.Setup(x => x.SetName(It.IsAny<int>(), It.IsAny<string>()))
            .Returns(Task.FromResult((CompanyModel?)null));

        var controller = new CompanyController(companyServiceMock.Object, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;


        var request = _fixture.GetUpdateCompanyNameRequest();
        var updateResponse = await controller.UpdateCompanyName(request);
        updateResponse.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<UpdateCompanyNameRequest>();
    }

    [Fact]
    public async Task ShouldUpdateCompanyNameReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var controller = new CompanyController(_companyService, _fixture.GetMapper());

        var request = _fixture.GetUpdateCompanyNameRequest();
        var updateResponse = await controller.UpdateCompanyName(request);

        updateResponse.Should().BeOfType<BadRequestObjectResult>();

    }

    [Fact]
    public async Task ShouldGetCompanyNameReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var controller = new CompanyController(_companyService, _fixture.GetMapper());

        var getResponse = await controller.GetCompanyName();

        getResponse.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task ShouldRegisterCompanyReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var companyServiceMock = new Mock<ICompanyService>();
        companyServiceMock.Setup(x => x.Register(It.IsAny<MemberModel>()))
            .Throws(new Exception());

        var controller = new CompanyController(companyServiceMock.Object, _fixture.GetMapper());

        var getResponse = await controller.RegisterCompany(_fixture.GetCompanyRegistrationRequest());

        getResponse.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldRegisterCompanyReturnBadRequestIfServiceReturnsNull()
    {
        _fixture.ClearDatabase();

        var companyServiceMock = new Mock<ICompanyService>();
        companyServiceMock.Setup(x => x.Register(It.IsAny<MemberModel>()))
            .Returns(Task.FromResult(((MemberModel?)null)));

        var controller = new CompanyController(companyServiceMock.Object, _fixture.GetMapper());

        var getResponse = await controller.RegisterCompany(_fixture.GetCompanyRegistrationRequest());

        getResponse.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<CompanyRegistrationRequest>();
    }
}