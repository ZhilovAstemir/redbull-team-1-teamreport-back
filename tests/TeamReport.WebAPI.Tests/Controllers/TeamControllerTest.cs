using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.WebAPI.Controllers;
using Xunit;

namespace TeamReport.WebAPI.Tests.Controllers;

public class TeamControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly TeamService _teamService;
    public TeamControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _teamService = new TeamService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapper());
    }

    [Fact]
    public void ShouldBeAbleToCreateController()
    {
        var controller = new TeamController(_teamService,_fixture.GetMapper());
        controller.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllMembers()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService,_fixture.GetMapper());

        await _teamService.Add(_fixture.GetMember());

        (await controller.GetAll()).Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<Member>>().Which.Count.Should().Be(1);
    }
}