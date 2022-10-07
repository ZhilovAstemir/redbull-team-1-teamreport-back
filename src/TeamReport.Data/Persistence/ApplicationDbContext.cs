using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;

namespace TeamReport.Data.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
        : base(option) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Leadership> Leaderships { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Week> Weeks { get; set; }

}

