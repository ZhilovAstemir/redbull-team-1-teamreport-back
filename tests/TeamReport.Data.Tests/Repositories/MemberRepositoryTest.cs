using FluentAssertions;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories;

namespace TeamReport.Data.Tests.Repositories;

public class MemberRepositoryTest
{
    private readonly RepositoryTestFixture _fixture;
    private readonly ApplicationDbContext _context;


    public MemberRepositoryTest()
    {
        _fixture = new RepositoryTestFixture();
        _context = _fixture.GetContext();
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberRepository()
    {
        var repository = new MemberRepository(_context);
        repository.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToAddMember()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };

        repository.Create(member);

        _context.Members.Should().Contain(member);
    }

    [Fact]
    public async Task ShouldBeAbleToGetMemberByEmail()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };
        repository.Create(member);

        var addedMember = await repository.ReadByEmail("email@email.com");

        addedMember.FirstName.Should().Be(member.FirstName);
    }

    [Fact]
    public async Task ShouldBeAbleToGetMemberById()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };
        var addedMember = await repository.Create(member);

        var gotMember = await repository.Read(addedMember.Id);

        gotMember?.Email.Should().Be(member.Email);
        gotMember?.FirstName.Should().Be(member.FirstName);
        gotMember?.LastName.Should().Be(member.LastName);
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllMembers()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };
        await repository.Create(member);

        var members = await repository.ReadAll();

        members.Should().Contain(member);
    }

    [Fact]
    public async Task ShouldBeAbleToUpdateMember()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };
        repository.Create(member);

        member.Email = "newEmail@gmail.com";

        (await repository.Update(member)).Should().BeTrue();

        var updatedMember = await repository.Read(member.Id);
        updatedMember.Email.Should().Be(member.Email);
    }

    [Fact]
    public async Task ShouldBeAbleToDeleteMember()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };
        await repository.Create(member);
        (await repository.Read(member.Id)).Should().NotBeNull();

        (await repository.Delete(member.Id)).Should().BeTrue();

        (await repository.Read(member.Id)).Should().BeNull();
    }

    [Fact]
    public async Task ShouldDeleteReturnFalseIfNothingToDelete()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        (await repository.Delete(0)).Should().BeFalse();
    }
}