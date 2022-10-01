using FluentAssertions;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class TeamServiceTest
{
    private readonly ServiceTestFixture _fixture;

    public TeamServiceTest()
    {
        _fixture = new ServiceTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateTeamService()
    {
        var service = new TeamService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapperDomainMock().Object);
        service.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToAddAndGetMember()
    {
        var service = new TeamService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapperDomainMock().Object);
        var member = _fixture.GetMember();

        service.Add(member);

        var createdMember = service.Get(member.Id);
        createdMember?.Email.Should().Be(member.Email);
    }

    [Fact]
    public void ShouldBeAbleToGetAllMembers()
    {
        var repository = _fixture.GetMemberRepositoryMock();
        var service = new TeamService(repository.Object, _fixture.GetMapperDomainMock().Object);

        service.Add(_fixture.GetMember());
        service.Add(_fixture.GetMember());
        repository.Verify(x => x.Create(It.IsAny<Member>()), Times.Exactly(2));

        var members = service.GetAll();
        repository.Verify(x => x.ReadAll(), Times.Exactly(1));
    }
}