using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Entities;

namespace redbull_team_1_teamreport_back.Data.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
        : base(option) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Member> Members { get; set; } 
    public DbSet<Leadership> Leaderships { get; set; } 
    public DbSet<Report> Reports { get; set; } 
    public DbSet<Week> Weeks { get; set; } 

}
