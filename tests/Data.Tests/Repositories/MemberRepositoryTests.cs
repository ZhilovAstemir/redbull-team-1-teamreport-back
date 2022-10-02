using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories;

namespace Data.Tests.Repositories;

public class MemberRepositoryTests
{
    private readonly ApplicationDbContext _context;

    public MemberRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new ApplicationDbContext(dbContextOptions);
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
        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };

        repository.Add(member);

        _context.Members.Should().Contain(member);
    }

    [Fact]
    public void ShouldBeAbleToGetMemberByEmail()
    {
        var repository = new MemberRepository(_context);

        var member = new Member()
        {
            Email = "email@email.com",
            FirstName = "FirstName",
            LastName = "LastName",
            Password = "Password"
        };
        repository.Add(member);

        var addedMember = repository.GetByEmail("email@email.com");

        addedMember.FirstName.Should().Be(member.FirstName);
    }
}