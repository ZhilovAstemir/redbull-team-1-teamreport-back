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

namespace TeamReport.WebAPI.Tests.Controllers;
public class MemberControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IMemberService _service;
    private readonly IEmailService _emailService;

    public MemberControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _service = new MemberService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapperMock());
        _emailService = new EmailService(_fixture.GetNewOptions());
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberController()
    {
        var controller = new MemberController(_service, _fixture.GetMapperMock(), _emailService);
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

}
