using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Data.Entities;
using TeamReport.Data.Enums;
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


    public Mock<IReportRepository> GetReportRepositoryMock()
    {
        var repositoryMock = new Mock<IReportRepository>();
        repositoryMock.Setup(x => x.Create(It.IsAny<Report>(), It.IsAny<Week>(), It.IsAny<Member>())).ReturnsAsync(1);
        repositoryMock.Setup(x => x.GetMemberReportsById(It.IsAny<int>())).ReturnsAsync(GetReportsList);
        return repositoryMock;
    }

    public Mock<IWeekRepository> GetWeekRepositoryMock()
    {
        var repositoryMock = new Mock<IWeekRepository>();
        repositoryMock.Setup(x => x.Add(It.IsAny<Week>())).ReturnsAsync(5);
        repositoryMock.Setup(x => x.GetWeekByEndDate(It.IsAny<DateTime>())).Returns(Task.FromResult(GetWeek()));
        return repositoryMock;
    }

    public ReportModel GetReportModel()
    {
        WeekModel week = new WeekModel()
        {
            DateEnd = new DateTime(2022, 11, 10),
            DateStart = new DateTime(2022, 11, 03)
        };
        return new ReportModel()
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
            Week = week
        };
    }

    public Week GetWeek()
    {
        return new Week()
        {
            DateEnd = new DateTime(2022, 10, 10),
            DateStart = new DateTime(2022, 10, 03)
        };
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

    public Member GetMemberWithId()
    {
        return new Member()
        {
            Id = 1,
            Email = "email@email.com",
            Password = PasswordHash.HashPassword("password"),
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Company = new Company() { Id = 1, Name = "CompanyName" }
        };
    }

    public List<Report> GetReportsList()
    {
        Member member = new Member()
        {
            Id = 1,
            Email = "email@email.com",
            Password = PasswordHash.HashPassword("password"),
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Company = new Company() { Id = 1, Name = "CompanyName" }
        };
        Week week = new Week()
        {
            DateEnd = new DateTime(2022, 11, 10),
            DateStart = new DateTime(2022, 11, 03)
        };
        Report report = new Report
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
            Member = member, 
            Week = week
        };
        List<Report> reports = new List<Report>();
        reports.Add(report);

        return reports;
    }
}
