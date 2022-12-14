using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Models;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Controllers;

namespace TeamReport.WebAPI.Tests.Controllers;
public class MemberControllerTest
{
    private readonly ControllerTestFixture _fixture;
    private readonly IMemberService _memberService;
    private readonly ITeamService _teamService;
    private readonly IEmailService _emailService;
    private readonly IMemberRepository _memberRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ILeadershipRepository _leadershipRepository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public MemberControllerTest()
    {
        _fixture = new ControllerTestFixture();
        _context = _fixture.GetContext();
        _memberRepository = new MemberRepository(_context);
        _companyRepository = new CompanyRepository(_context);
        _leadershipRepository = new LeadershipRepository(_context);
        _mapper = _fixture.GetMapper();
        _memberService = new MemberService(_memberRepository, _companyRepository, _mapper);
        _emailService = new EmailService(_fixture.GetNewOptions());
        _teamService = new TeamService(_memberRepository, _leadershipRepository, _companyRepository, _mapper);
        _configuration = new ConfigurationManager();
    }

    [Fact]
    public void ShouldBeAbleToCreateMemberController()
    {
        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);
        controller.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldBeAbleToInviteMember()
    {
        _fixture.ClearDatabase();

        var invitedMember = _fixture.GetInviteMemberRequest();
        var memberController = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        memberController.ControllerContext = controllerContext;

        var actual = await memberController.InviteMember(invitedMember);
        actual.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();
    }

    [Fact]
    public async Task ShouldInviteMemberSendEmailIfUserDidNotContinueHisRegistration()
    {
        _fixture.ClearDatabase();

        var inviteMemberRequest = _fixture.GetInviteMemberRequest();
        var memberController = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        memberController.ControllerContext = controllerContext;

        var actual = await memberController.InviteMember(inviteMemberRequest);
        actual.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();

        var memberModel = (actual as OkObjectResult).Value as MemberModel;
        memberModel.Email.Should().Be(inviteMemberRequest.Email);
        memberModel.FirstName.Should().Be(inviteMemberRequest.FirstName);
        memberModel.LastName.Should().Be(inviteMemberRequest.LastName);

        inviteMemberRequest.FirstName = "Ivan";
        inviteMemberRequest.LastName = "Ivanov";

        var response = await memberController.InviteMember(inviteMemberRequest);
        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();

        var newResult = (response as OkObjectResult).Value as MemberModel;
        newResult.Email.Should().Be(inviteMemberRequest.Email);
        newResult.FirstName.Should().Be(inviteMemberRequest.FirstName);
        newResult.LastName.Should().Be(inviteMemberRequest.LastName);

    }

    [Fact]
    public async Task ShouldInviteMemberReturnBadRequestIfMemberAlreadyRegistered()
    {
        _fixture.ClearDatabase();

        var inviteMemberRequest = _fixture.GetInviteMemberRequest();
        var memberController = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        memberController.ControllerContext = controllerContext;

        var request = _fixture.GetInviteMemberRequest();
        request.Email = member.Email;
        var response = await memberController.InviteMember(request);
        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldInviteMemberReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var inviteMemberRequest = _fixture.GetInviteMemberRequest();
        var memberController = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var request = _fixture.GetInviteMemberRequest();
        var response = await memberController.InviteMember(request);
        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToRegister()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);
        var request = _fixture.GetMemberRegistrationRequest();
        var response = await controller.Register(request);

        response.Should().BeAssignableTo<OkObjectResult>();
        (response as OkObjectResult)?.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldBeAbleToLogin()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var memberRegistrationRequest = _fixture.GetMemberRegistrationRequest();
        var registerResponse = await controller.Register(memberRegistrationRequest);
        registerResponse.Should().BeAssignableTo<OkObjectResult>();

        _fixture.GetContext().Members.Should().Contain(x => x.Email == memberRegistrationRequest.Email);

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse = await controller.Login(loginRequest);

        loginResponse.Should().NotBeNull();
    }

    [Fact]
    public async Task ShouldLoginReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var serviceMock = new Mock<IMemberService>();
        serviceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

        var controller = new MemberController(serviceMock.Object, _mapper, _emailService, _teamService, _configuration);

        var loginRequest = _fixture.GetLoginRequest();
        var loginResponse = await controller.Login(loginRequest);

        loginResponse.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldRegisterReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var serviceMock = new Mock<IMemberService>();
        serviceMock.Setup(x => x.Register(It.IsAny<MemberModel>())).Throws(new Exception());

        var controller = new MemberController(serviceMock.Object, _mapper, _emailService, _teamService, _configuration);

        var registrationRequest = _fixture.GetMemberRegistrationRequest();
        var response = await controller.Register(registrationRequest);

        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldGetMemberInformation()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        dbContext.Members.Add(_fixture.GetMember());
        await dbContext.SaveChangesAsync();
        var member = dbContext.Members.First();
        httpContext.Items["Member"] = member;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var response = await controller.GetMemberInformation();

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();
    }

    [Fact]
    public async Task ShouldBeAbleToContinueRegistration()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        dbContext.Members.Add(member);
        await dbContext.SaveChangesAsync();
        var createdMember = dbContext.Members.First();
        httpContext.Items["Member"] = createdMember;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var request = _fixture.GetContinueRegistrationRequest();

        var response = await controller.ContinueRegistration(request);

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldGetMemberInformationReturnBadRequestIfNoMemberInContext()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var request = _fixture.GetContinueRegistrationRequest();

        var response = await controller.GetMemberInformation();

        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldContinueRegistrationReturnBadRequestIfNoMemberInContext()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var request = _fixture.GetContinueRegistrationRequest();

        var response = await controller.ContinueRegistration(request);

        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldGetMemberInformationById()
    {
        _fixture.ClearDatabase();

        _context.Members.Add(_fixture.GetMember());
        _context.SaveChanges();
        var createdMember = _context.Members.First();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var request = _fixture.GetContinueRegistrationRequest();

        var response = await controller.GetMemberInformation(createdMember.Id);

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();
    }

    [Fact]
    public async Task ShouldGetMemberInformationByIdReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var memberServiceMock = new Mock<IMemberService>();
        memberServiceMock.Setup(x => x.GetMemberById(It.IsAny<int>())).Throws<Exception>();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var response = await controller.GetMemberInformation(1);

        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }

    [Fact]
    public async Task ShouldEditMemberInformation()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var controllerContext = new ControllerContext();
        var httpContext = new DefaultHttpContext();
        var dbContext = _fixture.GetContext();
        var member = _fixture.GetMember();
        member.Title = null;
        member.Password = null;
        dbContext.Members.Add(member);
        await dbContext.SaveChangesAsync();
        var createdMember = dbContext.Members.First();
        httpContext.Items["Member"] = createdMember;
        controllerContext.HttpContext = httpContext;
        controller.ControllerContext = controllerContext;

        var request = _fixture.GetEditMemberInformationRequest();
        request.Id = createdMember.Id;

        var response = await controller.EditMemberInformation(request);

        response.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeOfType<MemberModel>();
        var resultModel = (response as OkObjectResult)?.Value as MemberModel;

        resultModel?.Id.Should().Be(request.Id);
        resultModel?.FirstName.Should().Be(request.FirstName);
        resultModel?.LastName.Should().Be(request.LastName);
        resultModel?.Title.Should().Be(request.Title);
    }

    [Fact]
    public async Task ShouldEditMemberInformationReturnBadRequestIfAnyException()
    {
        _fixture.ClearDatabase();

        var controller = new MemberController(_memberService, _mapper, _emailService, _teamService, _configuration);

        var request = _fixture.GetEditMemberInformationRequest();
        var response = await controller.EditMemberInformation(request);

        response.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().BeOfType<string>();
    }
}
