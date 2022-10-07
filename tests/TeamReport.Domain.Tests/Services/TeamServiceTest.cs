using AutoMapper;
using FluentAssertions;
using Moq;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Models;
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

    [Fact]
    public async Task ShouldBeAbleToUpdateMemberReporters()
    {
        _fixture.ClearDatabase();

        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = member.Company;
        member3 = await _memberRepository.Create(member3);

        var memberModel = _mapper.Map<Member, MemberModel>(member);
        var member2Model = _mapper.Map<Member, MemberModel>(member2);
        var member3Model = _mapper.Map<Member, MemberModel>(member3);
        await service.UpdateMemberReporters(member.Id, new List<int>() { member2.Id, member3.Id });

        var reporters = await service.GetMemberReporters(member.Id);
        reporters.Should().HaveCount(2).And.Contain(x => x.Id == member2.Id).And.Contain(x => x.Id == member3.Id);
    }

    [Fact]
    public async Task ShouldBeAbleToUpdateMemberLeaders()
    {
        _fixture.ClearDatabase();

        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        member2 = await _memberRepository.Create(member2);
        var member3 = _fixture.GetMember();
        member3.Company = member.Company;
        member3 = await _memberRepository.Create(member3);

        var memberModel = _mapper.Map<Member, MemberModel>(member);
        var member2Model = _mapper.Map<Member, MemberModel>(member2);
        var member3Model = _mapper.Map<Member, MemberModel>(member3);
        await service.UpdateMemberLeaders(member.Id, new List<int>() { member2.Id, member3.Id });

        var leaders = await service.GetMemberLeaders(member.Id);
        leaders.Should().HaveCount(2).And.Contain(x => x.Id == member2.Id).And.Contain(x => x.Id == member3.Id);
    }

    [Fact]
    public async Task ShouldGetMemberLeadersThrowExceptionIfMapperReturnNull()
    {
        _fixture.ClearDatabase();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<List<Member>, List<MemberModel>>(It.IsAny<List<Member>>())).Returns((List<MemberModel>?)null);
        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, mapperMock.Object);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());

        var getLeaders = async () => await service.GetMemberLeaders(member.Id);

        await getLeaders.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task ShouldGetMemberReportersThrowExceptionIfMapperReturnNull()
    {
        _fixture.ClearDatabase();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<List<Member>, List<MemberModel>>(It.IsAny<List<Member>>())).Returns((List<MemberModel>?)null);
        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, mapperMock.Object);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());

        var getReporters = async () => await service.GetMemberReporters(member.Id);

        await getReporters.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task ShouldUpdateMemberReportersThrowExceptionIfMapperReturnNull()
    {
        _fixture.ClearDatabase();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<List<Member>, List<MemberModel>>(It.IsAny<List<Member>>())).Returns((List<MemberModel>?)null);
        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, mapperMock.Object);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());

        var updateReporters = async () => await service.UpdateMemberReporters(member.Id, It.IsAny<List<int>>());

        await updateReporters.Should().ThrowAsync<AutoMapperMappingException>();
    }

    [Fact]
    public async Task ShouldUpdateMemberLeadersThrowExceptionIfMapperReturnNull()
    {
        _fixture.ClearDatabase();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<List<Member>, List<MemberModel>>(It.IsAny<List<Member>>())).Returns((List<MemberModel>?)null);
        var service = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, mapperMock.Object);
        service.Should().NotBeNull().And.BeOfType<TeamService>();

        var member = await _memberRepository.Create(_fixture.GetMember());

        var updateLeaders = async () => await service.UpdateMemberLeaders(member.Id, It.IsAny<List<int>>());

        await updateLeaders.Should().ThrowAsync<AutoMapperMappingException>();
    }
}