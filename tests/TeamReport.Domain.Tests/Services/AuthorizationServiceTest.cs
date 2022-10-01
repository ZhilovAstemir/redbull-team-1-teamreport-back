using FluentAssertions;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class AuthorizationServiceTest
{
    private readonly ServiceTestFixture _fixture;

    public AuthorizationServiceTest()
    {
        _fixture = new ServiceTestFixture();
    }

    [Fact]
    public void ShouldBeAbleToCreateAuthorizationService()
    {
        var service = new AuthorizationService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapperDomainMock().Object);
        service.Should().NotBeNull();
    }

    [Fact]
    public void ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var service = new AuthorizationService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapperDomainMock().Object);

        var memberModel = _fixture.GetMemberModel();

        service.Register(memberModel).Should().BeOfType(typeof(int));
    }

    [Fact]
    public void ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var service = new AuthorizationService(_fixture.GetMemberRepositoryMock().Object, _fixture.GetMapperDomainMock().Object);

        var memberModel = _fixture.GetMemberModel();

        service.Login(memberModel.Email,memberModel.Password).Should().BeOfType(typeof(MemberModel));
    }

    [Fact]
    public void ShouldLoginThrowInvalidCreditalsExceptionIfCanNotFindUserInDb()
    {
        _fixture.ClearDatabase();

        var repository = _fixture.GetMemberRepositoryMock();
        repository.Setup(x => x.ReadByEmail(It.IsAny<string>())).Returns((Member?)null);

        var service = new AuthorizationService(repository.Object, _fixture.GetMapperDomainMock().Object);

        var memberModel = _fixture.GetMemberModel();

        var loginAction= () => service.Login(memberModel.Email,memberModel.Password);
        loginAction.Should().Throw<InvalidCreditalsException>();
    }

    [Fact]
    public void ShouldLoginThrowInvalidCreditalsExceptionIfWrongPassword()
    {
        _fixture.ClearDatabase();

        var repository = _fixture.GetMemberRepositoryMock();

        var service = new AuthorizationService(repository.Object, _fixture.GetMapperDomainMock().Object);

        var memberModel = _fixture.GetMemberModel();
        memberModel.Password = "newwrongpass";

        var loginAction= () => service.Login(memberModel.Email,memberModel.Password);

        loginAction.Should().Throw<InvalidCreditalsException>();
    }

    [Fact]
    public void ShouldGetTokenThrowDataExceptionIfInvalidModel()
    {
        _fixture.ClearDatabase();

        var repository = _fixture.GetMemberRepositoryMock();

        var service = new AuthorizationService(repository.Object, _fixture.GetMapperDomainMock().Object);

        var memberModel = _fixture.GetMemberModel();
        memberModel.Email = null;

        var getTokenAction= () => service.GetToken(memberModel);

        getTokenAction.Should().Throw<DataException>();
    }
}