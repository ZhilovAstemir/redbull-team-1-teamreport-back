using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Auth;
using TeamReport.Domain.Models;

namespace TeamReport.Domain.Tests.Services;

public class ServiceTestFixture
{
    public ApplicationDbContext GetContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new ApplicationDbContext(dbContextOptions);
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
            Password = "password"
        };
    }

    public Mock<IMemberRepository> GetMemberRepositoryMock()
    {
        var repositoryMock = new Mock<IMemberRepository>();
        repositoryMock.Setup(x => x.Read(It.IsAny<int>())).Returns(GetMember());
        repositoryMock.Setup(x => x.Create(It.IsAny<Member>())).Returns(GetMember());
        repositoryMock.Setup(x => x.ReadByEmail(It.IsAny<string>())).Returns(GetMember());
        repositoryMock.Setup(x => x.ReadAll()).Returns(new List<Member>() { GetMember() });
        return repositoryMock;
    }

    public Mock<IMapper> GetMapperDomainMock()
    {
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<Member, MemberModel>(It.IsAny<Member>())).Returns(GetMemberModel());
        mapperMock.Setup(x => x.Map<MemberModel, Member>(It.IsAny<MemberModel>())).Returns(GetMember());
        return mapperMock;
    }
}