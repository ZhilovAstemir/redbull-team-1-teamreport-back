using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;

namespace TeamReport.WebAPI.Tests.Controllers;

public class MemberControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IMemberService _service;

    public MemberControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _service = new MemberService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapperMock());
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberController()
    {
        var controller = new MemberController(_service, _fixture.GetMapperMock());
        controller.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_service, _fixture.GetMapperMock());
        var request = _fixture.GetMemberRegistrationRequest();
        var response=await controller.Register(request);

        response.Should().BeAssignableTo<OkObjectResult>();
        (response as OkObjectResult)?.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_service, _fixture.GetMapperMock());

        var memberRegistrationRequest = _fixture.GetMemberRegistrationRequest();
        var registerResponse=await controller.Register(memberRegistrationRequest);
        registerResponse.Should().BeAssignableTo<OkObjectResult>();

        _fixture.GetContext().Members.Should().Contain(x => x.Email == memberRegistrationRequest.Email);

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse=await controller.Login(loginRequest);

        loginResponse.Should().NotBeNull();
    }
}