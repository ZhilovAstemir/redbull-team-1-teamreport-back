using AutoMapper;
using FluentAssertions;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class TeamServiceTest
{
    private readonly ServiceTestFixture _fixture;
    private readonly ApplicationDbContext _context;
    private readonly IMemberRepository _memberRepository;
    private readonly ILeadershipRepository _leadershipRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public TeamServiceTest()
    {
        _fixture = new ServiceTestFixture();
        _context = _fixture.GetContext();
        _memberRepository = new MemberRepository(_context);
        _leadershipRepository = new LeadershipRepository(_context);
        _companyRepository = new CompanyRepository(_context);
        _mapper = _fixture.GetMapper();
    }

    [Fact]
    public void ShouldBeAbleToCreateTeamService()
    {
        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        service.Should().NotBeNull().And.BeOfType<TeamService>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllTeamMembers()
    {
        _fixture.ClearDatabase();

        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        await _memberRepository.Create(member2);

        var teamMembers = await service.GetAllTeamMembers(member.Company.Id);
        teamMembers.Should().HaveCount(2);
    }

    [Fact]
    public async Task ShouldBeAbleToGetMemberLeaders()
    {
        _fixture.ClearDatabase();

        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = member.Company;
        await _memberRepository.Create(member3);

        await _leadershipRepository.Create(new Leadership() { Leader = member2, Member = member });
        await _leadershipRepository.Create(new Leadership() { Leader = member3, Member = member });


        var leaders = await service.GetMemberLeaders(member.Id);
        leaders.Should().HaveCount(2).And.Contain(x => x.Id == member2.Id).And.Contain(x => x.Id == member3.Id);
    }

    [Fact]
    public async Task ShouldBeAbleToGetMemberReporters()
    {
        _fixture.ClearDatabase();

        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = member.Company;
        await _memberRepository.Create(member3);

        await _leadershipRepository.Create(new Leadership() { Leader = member, Member = member2 });
        await _leadershipRepository.Create(new Leadership() { Leader = member, Member = member3 });


        var reporters = await service.GetMemberReporters(member.Id);
        reporters.Should().HaveCount(2).And.Contain(x => x.Id == member2.Id).And.Contain(x => x.Id == member3.Id);
    }
}