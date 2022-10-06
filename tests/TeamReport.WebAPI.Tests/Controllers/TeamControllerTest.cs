using FluentAssertions;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Domain.Services;
using TeamReport.WebAPI.Controllers;

namespace TeamReport.WebAPI.Tests.Controllers;

public class TeamControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly TeamService _teamService;
    private readonly ApplicationDbContext _context;
    public TeamControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _context = _fixture.GetContext();
        _teamService = new TeamService(new MemberRepository(_context), new LeadershipRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());
    }

    [Fact]
    public void ShouldBeAbleToCreateController()
    {
        var controller = new TeamController(_teamService, _fixture.GetMapper());
        controller.Should().NotBeNull().And.BeOfType<TeamController>();
    }

}