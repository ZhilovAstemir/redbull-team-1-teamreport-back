using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;
using TeamReport.WebAPI.MapperStorage;

namespace WebAPI.Tests.Controllers;

public class AuthorizationControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IAuthorizationService _authService;

    public AuthorizationControllerTest()
    {
        _fixture=new ControllerTestFixture();
        _authService = new AuthorizationService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapperDomainMock());
    }

    [Fact]
    public void ShouldBeAbleToCreateAuthorizationController()
    {
        var controller = new AuthorizationController(_authService,_fixture.GetMapperApiMock());
        controller.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var controller = new AuthorizationController(_authService,_fixture.GetMapperApiMock());
        var request = _fixture.GetMemberRegistrationRequest();
        var response=controller.Register(request);

        response.Should().BeAssignableTo<OkObjectResult>();
        (response as OkObjectResult)?.Value.Should().BeOfType<int>();
    }

    [Fact]
    public void ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var controller = new AuthorizationController(_authService, _fixture.GetMapperApiMock());

        var memberRegistrationRequest = _fixture.GetMemberRegistrationRequest();
        var registerResponse=controller.Register(memberRegistrationRequest);
        registerResponse.Should().BeAssignableTo<OkObjectResult>();

        _fixture.GetContext().Members.Should().Contain(x => x.Email == memberRegistrationRequest.Email);

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse=controller.Login(loginRequest);

        loginResponse.Should().NotBeNull();
    }

}