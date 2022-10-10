using FluentAssertions;
using Moq;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Exceptions;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;

namespace TeamReport.Domain.Tests.Services;

public class MemberServiceTest
{
    private readonly ServiceTestFixture _fixture;
    private readonly ApplicationDbContext _context;

    public MemberServiceTest()
    {
        _fixture = new ServiceTestFixture();
        _context = _fixture.GetContext();
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberService()
    {
        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());
        service.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();

        (await service.Register(memberModel)).Should().BeOfType<MemberModel>();
    }

    [Fact]
    public async Task ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Password = "password";

        await service.Register(memberModel);

        (await service.Login(memberModel.Email, memberModel.Password)).Should().BeOfType(typeof(MemberModel));
    }

    [Fact]
    public void ShouldLoginThrowInvalidCreditalsExceptionIfCanNotFindUserInDb()
    {
        _fixture.ClearDatabase();

        var repository = new Mock<IMemberRepository>();
        repository.Setup(x => x.ReadByEmail(It.IsAny<string>())).Returns(Task.FromResult((Member?)null));

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();

        var loginAction = async () => await service.Login(memberModel.Email, memberModel.Password);
        loginAction.Should().ThrowAsync<InvalidCreditalsException>();
    }

    [Fact]
    public void ShouldLoginThrowInvalidCreditalsExceptionIfWrongPassword()
    {
        _fixture.ClearDatabase();

        var repository = new MemberRepository(_fixture.GetContext());

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Password = "newwrongpass";

        var loginAction = () => service.Login(memberModel.Email, memberModel.Password);

        loginAction.Should().ThrowAsync<InvalidCreditalsException>();
    }

    [Fact]
    public void ShouldGetTokenThrowDataExceptionIfInvalidModel()
    {
        _fixture.ClearDatabase();

        var repository = new MemberRepository(_fixture.GetContext());

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberModel = _fixture.GetMemberModel();
        memberModel.Email = null;

        var getTokenAction = () => service.GetToken(memberModel);

        getTokenAction.Should().ThrowAsync<DataException>();
    }

    [Fact]
    public async Task ShouldBeAbleToGetAllMembers()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        var member2 = _fixture.GetMember();
        member2.Company.Id = 0;
        _context.Members.Add(member2);
        await _context.SaveChangesAsync();

        var memberModels = await service.GetAll();

        memberModels.Should().HaveCount(2);
    }

    [Fact]
    public async Task ShouldBeAbleToContinueMemberRegistration()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var memberModels = await service.GetAll();
        memberModels.Should().HaveCount(1);

        var model = memberModels.First();

        model.Title = "Title";
        model.Password = "Password!";

        var registeredMemberModel = await service.ContinueRegistration(model);

        var login = async () => await service.Login(registeredMemberModel.Email, model.Password);
        await login.Should().NotThrowAsync("Login success");
    }

    [Fact]
    public async Task ShouldLoginThrowExceptionIfWrongPassword()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var memberModels = await service.GetAll();
        memberModels.Should().HaveCount(1);

        var model = memberModels.First();

        model.Title = "Title";
        model.Password = "Password!";

        var registeredMemberModel = await service.ContinueRegistration(model);

        var login = async () => await service.Login(registeredMemberModel.Email, "WrongPass");
        await login.Should().ThrowAsync<InvalidCreditalsException>("Login failed because of wrong password");
    }

    [Fact]
    public async Task ShouldContinueRegistrationThrowExceptionIfRepoCanNotFindMember()
    {
        _fixture.ClearDatabase();

        var memberRepositoryMock = new Mock<IMemberRepository>();
        memberRepositoryMock.Setup(x => x.Read(It.IsAny<int>())).Returns(Task.FromResult((Member?)null));
        var service = new MemberService(memberRepositoryMock.Object, new CompanyRepository(_context), _fixture.GetMapper());

        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var createdMember = _context.Members.First();
        var mapper = _fixture.GetMapper();
        var model = mapper.Map<Member, MemberModel>(createdMember);

        model.Title = "Title";
        model.Password = "Password!";

        var continueRegistration = async () => await service.ContinueRegistration(model);

        await continueRegistration.Should().ThrowAsync<EntityNotFoundException>();
    }


    [Fact]
    public async Task ShouldContinueRegistrationThrowExceptionIfRepoCanNotUpdateMember()
    {
        _fixture.ClearDatabase();

        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var createdMember = _context.Members.First();
        var mapper = _fixture.GetMapper();
        var model = mapper.Map<Member, MemberModel>(createdMember);

        model.Title = "Title";
        model.Password = "Password!";

        var memberRepositoryMock = new Mock<IMemberRepository>();
        memberRepositoryMock.Setup(x => x.Read(It.IsAny<int>())).Returns(Task.FromResult(createdMember));
        memberRepositoryMock.Setup(x => x.Update(It.IsAny<Member>())).Returns(Task.FromResult(false));
        var service = new MemberService(memberRepositoryMock.Object, new CompanyRepository(_context), _fixture.GetMapper());

        var continueRegistration = async () => await service.ContinueRegistration(model);

        await continueRegistration.Should().ThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async Task ShouldRegisterThrowUsedEmailException()
    {
        _fixture.ClearDatabase();

        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var createdMember = _context.Members.First();
        var mapper = _fixture.GetMapper();
        var model = mapper.Map<Member, MemberModel>(createdMember);

        model.Title = "Title";
        model.Password = "Password!";

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var registration = async () => await service.Register(model);

        await registration.Should().ThrowAsync<UsedEmailException>();
    }

    [Fact]
    public async Task ShouldGetToken()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var token = await service.GetToken(_fixture.GetMemberModel());

        token.Should().NotBeNull().And.BeOfType<string>();
    }

    [Fact]
    public async Task ShouldGetMemberByEmail()
    {
        _fixture.ClearDatabase();

        var member = _fixture.GetMember();
        _context.Members.Add(member);
        await _context.SaveChangesAsync();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberByEmail = await service.GetMemberByEmail(member.Email);
        memberByEmail.Should().NotBeNull().And.BeOfType<MemberModel>();
    }

    [Fact]
    public async Task ShouldGetMemberByEmailReturnNullIfCanNotFindMember()
    {
        _fixture.ClearDatabase();

        var service = new MemberService(new MemberRepository(_context), new CompanyRepository(_context), _fixture.GetMapper());

        var memberByEmail = await service.GetMemberByEmail("some email");
        memberByEmail.Should().BeNull();
    }

    [Fact]
    public async Task ShouldEditMemberInformation()
    {
        _fixture.ClearDatabase();

        var memberRepository = new MemberRepository(_context);
        var mapper = _fixture.GetMapper();
        var service = new MemberService(memberRepository, new CompanyRepository(_context), mapper);

        var member = _fixture.GetMember();
        var creatdMember = await memberRepository.Create(member);
        var model = mapper.Map<Member, MemberModel>(creatdMember);
        model.Should().NotBeNull();

        model.FirstName = "NewFirstName";
        model.LastName = "NewLastName";
        model.Title = "NewTitle";

        var updated = await service.EditMemberInformation(model);
        updated.FirstName.Should().Be(model.FirstName);
        updated.LastName.Should().Be(model.LastName);
        updated.Title.Should().Be(model.Title);
    }

    [Fact]
    public async Task ShouldEditMemberInformationThrowExceptionIfMemberNotFound()
    {
        _fixture.ClearDatabase();

        var memberRepository = new MemberRepository(_context);
        var mapper = _fixture.GetMapper();
        var service = new MemberService(memberRepository, new CompanyRepository(_context), mapper);

        var model = _fixture.GetMemberModel();
        model.Id = 0;

        var updated = async () => await service.EditMemberInformation(model);
        await updated.Should().ThrowAsync<EntityNotFoundException>();
    }
}