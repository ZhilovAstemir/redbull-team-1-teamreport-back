﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using redbull_team_1_teamreport_back.Data.Entities;
using redbull_team_1_teamreport_back.Data.Persistence;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.Domain.Models;
using TeamReport.Domain.Models.Requests;
using TeamReport.WebAPI.Mappers;
using TeamReport.WebAPI.Models;

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

    public Mock<IMemberRepository> GetMemberRepositoryMock()
    {
        var repositoryMock = new Mock<IMemberRepository>();
        repositoryMock.Setup(x => x.Read(It.IsAny<int>())).Returns(Task.FromResult(GetMember()));
        repositoryMock.Setup(x => x.Create(It.IsAny<Member>())).Returns(Task.FromResult(GetMember()));
        repositoryMock.Setup(x => x.ReadByEmail(It.IsAny<string>())).Returns(Task.FromResult(GetMember()));
        repositoryMock.Setup(x => x.ReadAll()).Returns(Task.FromResult(new List<Member>() { GetMember() }));
        return repositoryMock;
    }

    public IMapper GetMapper()
    {
        var mapperConfig = new MapperConfiguration(cfg => {
            cfg.AddProfile<MapperDomain>();
            cfg.AddProfile<MapperAPI>();
        });
        var mapper = new Mapper(mapperConfig);

        return mapper;
    }
}