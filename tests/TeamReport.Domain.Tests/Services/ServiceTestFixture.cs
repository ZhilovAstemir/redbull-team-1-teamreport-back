using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Models;
using TeamReport.WebAPI.Mappers;

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
            Title = "Title",
            Company = new Company() { Id = 1, Name = "CompanyName" }
        };
    }

    public Company GetCompany()
    {
        return new Company()
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
    public CompanyModel GetCompanyModel()
    {
        return new CompanyModel()
        {
            Id = 1,
            Name = "CompanyName"
        };
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


}