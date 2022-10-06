using FluentAssertions;
using Moq;
using TeamReport.Data.Entities;
using TeamReport.Data.Repositories;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class MemberServiceTest
{
    private readonly ServiceTestFixture _fixture;

    public MemberServiceTest()
    {
        _fixture = new ServiceTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberService()
    {
        var service = new MemberService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapper());
        service.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();

        (await service.Register(memberModel)).Should().BeOfType(typeof(Member));
    }

    [Fact]
    public async Task ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_fixture.GetContext()), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Password = "password";

        await service.Register(memberModel);

        (await service.Login(memberModel.Email, memberModel.Password)).Should().BeOfType(typeof(MemberModel));
    }

    [Fact]
    public void ShouldLoginThrowInvalidCreditalsExceptionIfCanNotFindUserInDb()
    {
        _fixture.ClearDatabase();

        var repository = _fixture.GetMemberRepositoryMock();
        repository.Setup(x => x.ReadByEmail(It.IsAny<string>())).Returns(Task.FromResult((Member?)null));

        var service = new MemberService(repository.Object, _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();

        var loginAction = async () => await service.Login(memberModel.Email, memberModel.Password);
        loginAction.Should().ThrowAsync<InvalidCreditalsException>();
    }

    [Fact]
    public void ShouldLoginThrowInvalidCreditalsExceptionIfWrongPassword()
    {
        _fixture.ClearDatabase();

        var repository = _fixture.GetMemberRepositoryMock();

        var service = new MemberService(repository.Object, _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Password = "newwrongpass";

        var loginAction = () => service.Login(memberModel.Email, memberModel.Password);

        loginAction.Should().ThrowAsync<InvalidCreditalsException>();
    }

    [Fact]
    public void ShouldGetTokenThrowDataExceptionIfInvalidModel()
    {
        _fixture.ClearDatabase();

        var repository = _fixture.GetMemberRepositoryMock();

        var service = new MemberService(repository.Object, _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Email = null;

        var getTokenAction = () => service.GetToken(memberModel);

        getTokenAction.Should().ThrowAsync<DataException>();
    }
}