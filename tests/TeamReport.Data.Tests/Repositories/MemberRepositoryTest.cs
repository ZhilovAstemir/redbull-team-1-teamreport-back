using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
    public void ShouldBeAbleToGetMemberByEmail()
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

        var addedMember = repository.ReadByEmail("email@email.com");

        addedMember.FirstName.Should().Be(member.FirstName);
    }

    [Fact]
    public void ShouldBeAbleToGetMemberById()
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
        var addedMember = repository.Create(member);

        var gotMember = repository.Read(addedMember.Id);

        gotMember?.Email.Should().Be(member.Email);
        gotMember?.FirstName.Should().Be(member.FirstName);
        gotMember?.LastName.Should().Be(member.LastName);
    }

    [Fact]
    public void ShouldBeAbleToGetAllMembers()
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

        var members=repository.ReadAll();

        members.Should().Contain(member);
    }

    [Fact]
    public void ShouldBeAbleToUpdateMember()
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

        repository.Update(member).Should().BeTrue();

        var updatedMember = repository.Read(member.Id);
        updatedMember.Email.Should().Be(member.Email);
    }

    [Fact]
    public void ShouldBeAbleToDeleteMember()
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
        repository.Read(member.Id).Should().NotBeNull();

        repository.Delete(member.Id).Should().BeTrue();

        repository.Read(member.Id).Should().BeNull();
    }

    [Fact]
    public void ShouldDeleteReturnFalseIfNothingToDelete()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        repository.Delete(0).Should().BeFalse();
    }
}