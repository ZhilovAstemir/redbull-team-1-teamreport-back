using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Tests.Controllers;

public class TeamControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly TeamService _teamService;
    private readonly ApplicationDbContext _context;
    private readonly IMemberRepository _memberRepository;
    private readonly ILeadershipRepository _leadershipRepository;
    private readonly ICompanyRepository _companyRepository;

    public TeamControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _context = _fixture.GetContext();
        _memberRepository = new MemberRepository(_context);
        _leadershipRepository = new LeadershipRepository(_context);
        _companyRepository = new CompanyRepository(_context);
        _teamService = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _fixture.GetMapper());
    }

    [Fact]
    public void ShouldBeAbleToCreateController()
    {
        var controller = new TeamController(_teamService, _fixture.GetMapper());
        controller.Should().NotBeNull().And.BeOfType<TeamController>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllTeamMembers()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var member1 = _fixture.GetMember();
        member1 = await _memberRepository.Create(member1);
        var company = member1.Company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = company;
        member3 = await _memberRepository.Create(member3);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = member1;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var teamMembers = await controller.GetAllTeamMembers();
        teamMembers.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(3);
    }

    [Fact]
    public async Task ShouldGetAllTeamMembersReturnBadRequestIfCompanyNull()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var member1 = _fixture.GetMember();
        member1 = await _memberRepository.Create(member1);
        var company = member1.Company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = company;
        member3 = await _memberRepository.Create(member3);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();

        member1.Company = null;

        httpContext.Items["Member"] = member1;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var teamMembers = await controller.GetAllTeamMembers();
        teamMembers.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetMemberReporters()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var member1 = _fixture.GetMember();
        member1 = await _memberRepository.Create(member1);
        var company = member1.Company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = company;
        member3 = await _memberRepository.Create(member3);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = member1;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.GetMemberReporters(member1.Id);
        reporters.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(0);

        await _leadershipRepository.UpdateReporters(member1.Id, new List<int>() { member2.Id, member3.Id });

        reporters = await controller.GetMemberReporters(member1.Id);
        reporters.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(2);
    }

    [Fact]
    public async Task ShouldGetMemberReportersReturnBadRequestIfHttpContextWithoutMember()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = null;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.GetMemberReporters(It.IsAny<int>());
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetMemberLeaders()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var member1 = _fixture.GetMember();
        member1 = await _memberRepository.Create(member1);
        var company = member1.Company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = company;
        member3 = await _memberRepository.Create(member3);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = member1;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var leaders = await controller.GetMemberLeaders(member1.Id);
        leaders.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(0);

        await _leadershipRepository.UpdateLeaders(member1.Id, new List<int>() { member2.Id, member3.Id });

        leaders = await controller.GetMemberLeaders(member1.Id);
        leaders.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(2);
    }

    [Fact]
    public async Task ShouldGetMemberLeadersReturnBadRequestIfHttpContextWithoutMember()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = null;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.GetMemberLeaders(It.IsAny<int>());
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToUpdateMemberLeaders()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var member1 = _fixture.GetMember();
        member1 = await _memberRepository.Create(member1);
        var company = member1.Company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = company;
        member3 = await _memberRepository.Create(member3);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = member1;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var leaders = await controller.GetMemberLeaders(member1.Id);
        leaders.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(0);

        await controller.UpdateMemberLeaders(new MemberIdsListRequest()
        {
            MemberId = member1.Id,
            MembersIds = new List<int>() { member2.Id, member3.Id }
        });

        leaders = await controller.GetMemberLeaders(member1.Id);
        leaders.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(2);
    }

    [Fact]
    public async Task ShouldUpdateMemberLeadersReturnBadRequestIfHttpContextWithoutMember()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = null;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var leaders = await controller.UpdateMemberLeaders(new MemberIdsListRequest() { MemberId = 0, MembersIds = new List<int>() });
        leaders.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }


    [Fact]
    public async Task ShouldBeAbleToUpdateMemberReporters()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var member1 = _fixture.GetMember();
        member1 = await _memberRepository.Create(member1);
        var company = member1.Company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = company;
        member3 = await _memberRepository.Create(member3);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = member1;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.GetMemberReporters(member1.Id);
        reporters.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(0);

        await controller.UpdateMemberReporters(new MemberIdsListRequest()
        {
            MemberId = member1.Id,
            MembersIds = new List<int>() { member2.Id, member3.Id }
        });

        reporters = await controller.GetMemberReporters(member1.Id);
        reporters.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<List<MemberModel>>().Which.Count.Should().Be(2);
    }

    [Fact]
    public async Task ShouldUpdateMemberReportersReturnBadRequestIfHttpContextWithoutMember()
    {
        _fixture.ClearDatabase();

        var controller = new TeamController(_teamService, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = null;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.UpdateMemberReporters(new MemberIdsListRequest() { MemberId = 0, MembersIds = new List<int>() });
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldGetMemberReportersReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var teamServiceMock = new Mock<ITeamService>();
        teamServiceMock.Setup(x => x.GetMemberById(It.IsAny<int>())).Throws<Exception>();

        var controller = new TeamController(teamServiceMock.Object, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = _fixture.GetMember();
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.GetMemberReporters(0);
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldGetMemberLeadersReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var teamServiceMock = new Mock<ITeamService>();
        teamServiceMock.Setup(x => x.GetMemberById(It.IsAny<int>())).Throws<Exception>();

        var controller = new TeamController(teamServiceMock.Object, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = _fixture.GetMember();
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.GetMemberLeaders(0);
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldUpdateMemberLeadersReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var teamServiceMock = new Mock<ITeamService>();
        teamServiceMock.Setup(x => x.GetMemberById(It.IsAny<int>())).Throws<Exception>();

        var controller = new TeamController(teamServiceMock.Object, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = _fixture.GetMember();
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.UpdateMemberLeaders(new MemberIdsListRequest());
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldUpdateMemberReportersReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var teamServiceMock = new Mock<ITeamService>();
        teamServiceMock.Setup(x => x.GetMemberById(It.IsAny<int>())).Throws<Exception>();

        var controller = new TeamController(teamServiceMock.Object, _fixture.GetMapper());

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        httpContext.Items["Member"] = _fixture.GetMember();
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var reporters = await controller.UpdateMemberReporters(new MemberIdsListRequest());
        reporters.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }
}