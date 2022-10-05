using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using Respawn;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.Domain.Services.Interfaces;

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
            Title = "Title"
        };
    }

    public MemberModel GetMemberModel()
    {
        return new MemberModel()
        {
            Email = "email@email.com",
            Password = PasswordHash.HashPassword("password")
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
            Title = "Title"
        };
    }

    public LoginRequest GetLoginRequest()
    {
        return new LoginRequest() { Email = "email@email.com", Password = "password" };
    }

    public IMapper GetMapperMock()
    {
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Member, MemberModel>(It.IsAny<Member>())).Returns(GetMemberModel());
        mapperMock.Setup(x => x.Map<MemberModel, Member>(It.IsAny<MemberModel>())).Returns(GetMember());
        mapperMock.Setup(x => x.Map<MemberRegistrationRequest, MemberModel>(It.IsAny<MemberRegistrationRequest>())).Returns(GetMemberModel());

        return mapperMock.Object;
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
}