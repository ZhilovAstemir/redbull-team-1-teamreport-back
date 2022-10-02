using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using Respawn;
using Respawn.Graph;
using TeamReport.Domain;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Models.Requests;

namespace WebAPI.Tests.Controllers;

public class ControllerTestFixture
{
    private readonly Checkpoint _checkpoint;
    public ControllerTestFixture()
    {
        _checkpoint = new Checkpoint()
        {
            SchemasToInclude = new []{"dbo"}
        };
    }

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

    public IMapper GetMapperDomainMock()
    {
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Member, MemberModel>(It.IsAny<Member>())).Returns(GetMemberModel());
        mapperMock.Setup(x => x.Map<MemberModel, Member>(It.IsAny<MemberModel>())).Returns(GetMember());
        return mapperMock.Object;
    }

    public IMapper GetMapperApiMock()
    {
        var mapperMock = new Mock<IMapper>();
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
}