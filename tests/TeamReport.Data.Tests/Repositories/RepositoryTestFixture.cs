using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Persistence;

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
}