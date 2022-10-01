﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;

namespace TeamReport.WebAPI.Tests.Controllers;

public class AuthorizationControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IAuthorizationService _authService;

    public AuthorizationControllerTest()
    {
        _fixture=new ControllerTestFixture();
        _authService = new AuthorizationService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapperMock());
    }

    [Fact]
    public void ShouldBeAbleToCreateAuthorizationController()
    {
        var controller = new AuthorizationController(_authService,_fixture.GetMapperMock());
        controller.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var controller = new AuthorizationController(_authService,_fixture.GetMapperMock());
        var request = _fixture.GetMemberRegistrationRequest();
        var response=controller.Register(request);

        response.Should().BeAssignableTo<OkObjectResult>();
        (response as OkObjectResult)?.Value.Should().BeOfType<string>();
    }

    [Fact]
    public void ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var controller = new AuthorizationController(_authService, _fixture.GetMapperMock());

        var memberRegistrationRequest = _fixture.GetMemberRegistrationRequest();
        var registerResponse=controller.Register(memberRegistrationRequest);
        registerResponse.Should().BeAssignableTo<OkObjectResult>();

        _fixture.GetContext().Members.Should().Contain(x => x.Email == memberRegistrationRequest.Email);

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse=controller.Login(loginRequest);

        loginResponse.Should().NotBeNull();
    }

    [Fact]
    public void ShouldValidateToken()
    {
        _fixture.ClearDatabase();

        var controller = new AuthorizationController(_authService, _fixture.GetMapperMock());

        var memberRegistrationRequest= _fixture.GetMemberRegistrationRequest();

        var token=(controller.Register(memberRegistrationRequest) as OkObjectResult)?.Value as string;

        controller.ValidateToken(token).Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public void ShouldReturnUnauthorizedIfInvalidToken()
    {
        _fixture.ClearDatabase();

        var controller = new AuthorizationController(_authService, _fixture.GetMapperMock());

        controller.ValidateToken(It.IsAny<string>()).Should().BeOfType<UnauthorizedResult>();
    }

}