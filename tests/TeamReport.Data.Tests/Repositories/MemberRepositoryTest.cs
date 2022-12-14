using FluentAssertions;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;

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

    [Fact]
    public async Task ShouldReadMembersByCompanyId()
    {
        _fixture.ClearDatabase(_context);

        var repository = new MemberRepository(_context);

        var member = _fixture.GetMember();
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        member = await repository.Read(1);
        var member2 = _fixture.GetMember();
        member2.Company = member.Company;
        var member3 = _fixture.GetMember();
        member3.Company = member.Company;
        _context.Members.Add(member2);
        _context.Members.Add(member3);
        await _context.SaveChangesAsync();


        (await repository.ReadByCompany(member.Company.Id)).Should().HaveCount(3).And.Contain(member).And.Contain(member2).And.Contain(member3);
    }
}