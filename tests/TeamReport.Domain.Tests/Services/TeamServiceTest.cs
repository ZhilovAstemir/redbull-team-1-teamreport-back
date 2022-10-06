using FluentAssertions;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class TeamServiceTest
{
    private readonly ServiceTestFixture _fixture;
    private readonly ApplicationDbContext _context;

    public TeamServiceTest()
    {
        _fixture = new ServiceTestFixture();
        _context = _fixture.GetContext();
    }

    [Fact]
    public void ShouldBeAbleToCreateTeamService()
    {
        var service = new TeamService(new MemberRepository(_context), new LeadershipRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());
        service.Should().NotBeNull().And.BeOfType<TeamService>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllTeamMembers()
    {
        _fixture.ClearDatabase();

        var memberRepository = new MemberRepository(_context);
        var service = new TeamService(memberRepository, new LeadershipRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await memberRepository.Create(_fixture.GetMember());
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        await memberRepository.Create(member2);

        var teamMembers = await service.GetAllTeamMembers(member.Company.Id);
    }



}