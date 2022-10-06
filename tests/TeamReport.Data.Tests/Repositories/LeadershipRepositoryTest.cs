using FluentAssertions;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;

namespace TeamReport.Data.Tests.Repositories;

public class LeadershipRepositoryTest
{
    private readonly RepositoryTestFixture _fixture;
    private readonly ApplicationDbContext _context;


    public LeadershipRepositoryTest()
    {
        _fixture = new RepositoryTestFixture();
        _context = _fixture.GetContext();
    }

    [Fact]
    public void ShouldBeAbleToCreateLeadershipRepository()
    {
        var repository = new LeadershipRepository(_context);
        repository.Should().NotBeNull().And.BeOfType<LeadershipRepository>();
    }

    [Fact]
    public async Task ShouldBeAbleToCreateLeadership()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();

        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };

        var repository = new LeadershipRepository(_context);

        await repository.Create(leadership);

        _context.Leaderships.Should().Contain(leadership);
    }

    [Fact]
    public async Task ShouldBeAbleToReadLeadershipById()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };

        var repository = new LeadershipRepository(_context);

        var addedLeadership = await repository.Create(leadership);

        var gotLeadership = await repository.Read(addedLeadership.Id);

        gotLeadership?.Member.Id.Should().Be(leadership.Member.Id);
        gotLeadership?.Leader.Id.Should().Be(leadership.Leader.Id);
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllLeaderships()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };

        var repository = new LeadershipRepository(_context);

        await repository.Create(leadership);

        var leaderships = await repository.ReadAll();

        leaderships.Should().Contain(leadership);
    }

    [Fact]
    public async Task ShouldBeAbleToUpdateLeadership()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };

        var repository = new LeadershipRepository(_context);

        await repository.Create(leadership);

        leadership.Leader.LastName = "NewLeaderName";

        (await repository.Update(leadership)).Should().BeTrue();

        var updatedLeadership = await repository.Read(leadership.Id);
        updatedLeadership?.Leader.LastName.Should().Be(leadership.Leader.LastName);
    }

    [Fact]
    public async Task ShouldBeAbleToDeleteLeadership()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };

        var repository = new LeadershipRepository(_context);

        await repository.Create(leadership);
        (await repository.Read(leadership.Id)).Should().NotBeNull();

        (await repository.Delete(leadership.Id)).Should().BeTrue();

        (await repository.Read(leadership.Id)).Should().BeNull();
    }

    [Fact]
    public async Task ShouldDeleteReturnFalseIfNothingToDelete()
    {
        _fixture.ClearDatabase(_context);

        var repository = new LeadershipRepository(_context);

        (await repository.Delete(0)).Should().BeFalse();
    }

    [Fact]
    public async Task ShouldReadLeadersOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        var leader2 = _fixture.GetMember();
        leader2.Company = company;
        _context.Members.Add(leader2);
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader2, Member = member };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        var leadersOfMember = await repository.ReadLeaders(member.Id);
        leadersOfMember.Should().HaveCount(2);
        leadersOfMember.Should().Contain(leader).And.Contain(leader2);
    }


    [Fact]
    public async Task ShouldReadReportersOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var member = _fixture.GetMember();
        member.Company = company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        _context.Members.Add(member2);
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader, Member = member2 };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        var reportersOfMember = await repository.ReadReporters(leader.Id);
        reportersOfMember.Should().HaveCount(2);
        reportersOfMember.Should().Contain(member).And.Contain(member2);
    }

    [Fact]
    public async Task ShouldDeleteAllLeadershipsOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var leader2 = _fixture.GetMember();
        leader2.Company = company;
        var member = _fixture.GetMember();
        member.Company = company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        _context.Members.Add(member2);
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader, Member = member2 };
        var leadership3 = new Leadership() { Id = 3, Leader = leader2, Member = member2 };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.Leaderships.Add(leadership3);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        (await repository.DeleteLeaderships(member2.Id)).Should().BeTrue();

        _context.Leaderships.Should().HaveCount(1).And.Contain(leadership);
    }

    [Fact]
    public async Task ShouldDeleteAllLeadersOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var leader2 = _fixture.GetMember();
        leader2.Company = company;
        var member = _fixture.GetMember();
        member.Company = company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        _context.Members.Add(member2);
        _context.Members.Add(member);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader, Member = member2 };
        var leadership3 = new Leadership() { Id = 3, Leader = leader2, Member = member2 };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.Leaderships.Add(leadership3);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        (await repository.DeleteLeaders(member.Id)).Should().BeTrue();

        _context.Leaderships.Should().HaveCount(2).And.NotContain(leadership);
    }

    [Fact]
    public async Task ShouldDeleteAllReportersOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var leader2 = _fixture.GetMember();
        leader2.Company = company;
        var member = _fixture.GetMember();
        member.Company = company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        _context.Members.Add(member2);
        _context.Members.Add(member);
        _context.Members.Add(leader2);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader, Member = member2 };
        var leadership3 = new Leadership() { Id = 3, Leader = leader2, Member = member2 };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.Leaderships.Add(leadership3);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        (await repository.DeleteReporters(leader.Id)).Should().BeTrue();

        _context.Leaderships.Should().HaveCount(1).And.Contain(leadership3);
    }

    [Fact]
    public async Task ShouldUpdateReportersOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var leader2 = _fixture.GetMember();
        leader2.Company = company;
        var member = _fixture.GetMember();
        member.Company = company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        _context.Members.Add(member2);
        _context.Members.Add(member);
        _context.Members.Add(leader2);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader, Member = member2 };
        var leadership3 = new Leadership() { Id = 3, Leader = leader2, Member = member2 };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.Leaderships.Add(leadership3);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        var updated = await repository.UpdateReporters(leader.Id, new List<Member>() { member });

        updated.Should().HaveCount(1).And.Contain(member).And.NotContain(member2);
    }

    [Fact]
    public async Task ShouldUpdateLeadersOfMember()
    {
        _fixture.ClearDatabase(_context);

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var leader = _context.Members.First();
        var company = leader.Company;
        var leader2 = _fixture.GetMember();
        leader2.Company = company;
        var member = _fixture.GetMember();
        member.Company = company;
        var member2 = _fixture.GetMember();
        member2.Company = company;
        _context.Members.Add(member2);
        _context.Members.Add(member);
        _context.Members.Add(leader2);
        _context.SaveChanges();

        var leadership = new Leadership() { Id = 1, Leader = leader, Member = member };
        var leadership2 = new Leadership() { Id = 2, Leader = leader, Member = member2 };
        var leadership3 = new Leadership() { Id = 3, Leader = leader2, Member = member2 };

        _context.Leaderships.Add(leadership);
        _context.Leaderships.Add(leadership2);
        _context.Leaderships.Add(leadership3);
        _context.SaveChanges();

        var repository = new LeadershipRepository(_context);

        var updated = await repository.UpdateLeaders(member2.Id, new List<Member>() { leader2 });

        updated.Should().HaveCount(1).And.Contain(leader2).And.NotContain(leader);
    }
}