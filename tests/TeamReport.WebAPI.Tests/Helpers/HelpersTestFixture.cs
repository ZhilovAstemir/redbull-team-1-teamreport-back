using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
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
        var config = new MapperConfiguration(cfg => cfg.AddProfiles(new List<Profile>(){new MapperDomain(),new MapperAPI()}));
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

    //public MemberModel GetMemberModel()
    //{
    //    return new MemberModel()
    //    {
    //        Email = "email@email.com",
    //        Password = PasswordHash.HashPassword("password")
    //    };
    //}
}