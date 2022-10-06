using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Mappers;

namespace TeamReport.WebAPI.Tests.Helpers;

public class HelpersTestFixture
{
    public ApplicationDbContext GetContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new ApplicationDbContext(dbContextOptions);
    }

    public IMapper GetMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>() { new MapperDomain(), new MapperAPI() }));
        return config.CreateMapper();
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

    public ITeamService GetTeamService()
    {
        var context = GetContext();
        return new TeamService(new MemberRepository(context), new LeadershipRepository(context),
            new CompanyRepository(context), GetMapper());
    }
}