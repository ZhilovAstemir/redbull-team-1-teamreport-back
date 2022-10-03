using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using Respawn;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.Domain.Services.Interfaces;
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
}