using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using TeamReport.Data.Entities;
using TeamReport.Data.Enums;
using TeamReport.Data.Persistence;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Mappers;
using TeamReport.WebAPI.Models;

namespace TeamReport.WebAPI.Tests.Controllers;

public class ControllerTestFixture
{
    public void ClearDatabase()
    {
        var context = GetContext();
        context.Database.EnsureDeleted();
        context.SaveChanges();
    }

    public Member GetMember()
    {
        return new Member()
        {
            Email = "email@email.com",
            Password = PasswordHash.HashPassword("password"),
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Company = GetCompany()
        };
    }

    public Company GetCompany()
    {
        return new Company()
        {
            Name = "CompanyName"
        };
    }

    public CompanyModel GetCompanyModel()
    {
        return new CompanyModel()
        {
            Id = 1,
            Name = "CompanyName"
        };
    }

    public MemberModel GetMemberModel()
    {
        return new MemberModel()
        {
            Email = "email@email.com",
            Password = PasswordHash.HashPassword("password"),
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Company = GetCompanyModel()
        };
    }

    public MemberRegistrationRequest GetMemberRegistrationRequest()
    {
        return new MemberRegistrationRequest()
        {
            Email = "email@email.com",
            Password = "password",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            CompanyId = 1
        };
    }

    public LoginRequest GetLoginRequest()
    {
        return new LoginRequest() { Email = "email@email.com", Password = "password" };
    }

    public CompanyRegistrationRequest GetCompanyRegistrationRequest()
    {
        var member = GetMember();

        return new CompanyRegistrationRequest()
        {
            Email = member.Email,
            Company = member.Company,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Password = member.Password,
            Title = member.Title
        };
    }

    public ReportRequest GetReportRequest()
    {
        return new ReportRequest()
        {
            Morale = Emotion.Good,
            MoraleComment = "Good",
            Stress = Emotion.Low,
            StressComment = "Low",
            Workload = Emotion.Low,
            WorkloadComment = "Low",
            High = "High",
            Low = "Low",
            Else = "Else",
            EndDate = new DateTime(2022, 10, 10),
            StartDate = new DateTime(2022, 10, 03)
        };
    }

    public UpdateCompanyNameRequest GetUpdateCompanyNameRequest()
    {
        return new UpdateCompanyNameRequest() { NewCompanyName = "New Comapny Name" };
    }
    public ContinueRegistrationRequest GetContinueRegistrationRequest()
    {
        return new ContinueRegistrationRequest() { Password = "Password!", Title = "Title" };
    }

    public IMapper GetMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MapperDomain>();
            cfg.AddProfile<MapperAPI>();
        });
        var mapper = new Mapper(mapperConfig);

        return mapper;
    }

    public ApplicationDbContext GetContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new ApplicationDbContext(dbContextOptions);
    }

    public IOptions<EmailConfiguration> GetNewOptions()
    {
        var optionMock = new Mock<IOptions<EmailConfiguration>>();
        optionMock.Setup(x => x.Value).Returns(GetEmailConfiguration());

        return optionMock.Object;
    }

    public EmailConfiguration GetEmailConfiguration()
    {
        return new EmailConfiguration()
        {
            From = "teamreports111@gmail.com",
            SmtpServer = "smtp.gmail.com",
            Port = 465,
            UserName = "teamreports111@gmail.com",
            Password = "btpyftfbrhibrgan"
        };
    }

    public InviteMemberModelRequest GetInviteMemberRequest()
    {
        return new InviteMemberModelRequest()
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "misterjohn@gmail.com"
        };
    }

    public EditMemberInformationRequest GetEditMemberInformationRequest()
    {
        return new EditMemberInformationRequest()
        {
            Id = 1,
            FirstName = "NewFirstName",
            LastName = "NewLastName",
            Title = "NewTitle"
        };
    }
}