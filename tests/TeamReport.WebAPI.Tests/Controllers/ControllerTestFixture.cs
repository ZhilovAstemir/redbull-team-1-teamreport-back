using AutoMapper;
using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
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
            Title = "Title"
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

    public UpdateCompanyNameRequest GetUpdateCompanyNameRequest()
    {
        return new UpdateCompanyNameRequest() { NewCompanyName = "New Comapny Name" };
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


}