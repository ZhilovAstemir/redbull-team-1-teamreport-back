using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;

namespace TeamReport.Data.Tests.Repositories;

public class RepositoryTestFixture
{
    public ApplicationDbContext GetContext()
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new ApplicationDbContext(dbContextOptions);
    }

    public void ClearDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.SaveChanges();
    }

    public Company GetCompany()
    {
        return new Company() { Id = 1, Name = "CompanyName" };
    }

    public Leadership GetLeadership()
    {
        return new Leadership() { Id = 1, Leader = GetMember(), Member = GetMember() };
    }

    public Member GetMember()
    {
        return new Member()
        {
            Email = "email@email.com",
            Password = "password",
            FirstName = "FirstName",
            LastName = "LastName",
            Title = "Title",
            Company = new Company() { Id = 1, Name = "CompanyName" }
        };
    }
}