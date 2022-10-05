using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;
using TeamReport.WebAPI.MapperStorage;
using TeamReport.WebAPI.Models;
using Moq;
using TeamReport.Domain.Models;

namespace TeamReport.WebAPI.Tests.Controllers;
public class MemberControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IMemberService _service;
    private readonly IEmailService _emailService;

    public MemberControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _service = new MemberService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapper());
        _emailService = new EmailService(_fixture.GetNewOptions());
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberController()
    {
        var controller = new MemberController(_service, _fixture.GetMapper(), _emailService);
        controller.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToInviteMember()
    {
        var invitedMember = _fixture.GetInviteMemberRequest();
        var memberController = new MemberController(_service, _fixture.GetMapper(), _emailService);

        var actual = await memberController.InviteMember(invitedMember);
        var actualResult = actual as OkResult;

        Assert.NotNull(actualResult);
        Assert.Equal(StatusCodes.Status200OK, actualResult.StatusCode);
    }

    public async Task ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_service, _fixture.GetMapper());
        var request = _fixture.GetMemberRegistrationRequest();
        var response = await controller.Register(request);

        response.Should().BeAssignableTo<OkObjectResult>();
        (response as OkObjectResult)?.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_service, _fixture.GetMapper());

        var memberRegistrationRequest = _fixture.GetMemberRegistrationRequest();
        var registerResponse = await controller.Register(memberRegistrationRequest);
        registerResponse.Should().BeAssignableTo<OkObjectResult>();

        _fixture.GetContext().Members.Should().Contain(x => x.Email == memberRegistrationRequest.Email);

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse = await controller.Login(loginRequest);

        loginResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldLoginReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var serviceMock = new Mock<IMemberService>();
        serviceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

        var controller = new MemberController(serviceMock.Object, _fixture.GetMapper());

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse = await controller.Login(loginRequest);

        loginResponse.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<Exception>();
    }

    [Fact]
    public async Task ShouldRegisterReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var serviceMock = new Mock<IMemberService>();
        serviceMock.Setup(x => x.Register(It.IsAny<MemberModel>())).Throws(new Exception());

        var controller = new MemberController(serviceMock.Object, _fixture.GetMapper());

        var registrationRequest = _fixture.GetMemberRegistrationRequest();
        var response = await controller.Register(registrationRequest);

        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<Exception>();
    }
}
