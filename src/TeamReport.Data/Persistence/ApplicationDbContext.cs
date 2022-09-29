using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using redbull_team_1_teamreport_back.Domain.Entities;
using redbull_team_1_teamreport_back.Domain.Identity;

namespace redbull_team_1_teamreport_back.Domain.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
        : base(option)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Member> Members { get; set; } 
    public DbSet<Leadership> Leaderships { get; set; } 
    public DbSet<Report> Reports { get; set; } 
    public DbSet<Week> Weeks { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable(nameof(Member));
            entity.HasKey(c => c.Id);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable(nameof(Company));
            entity.HasKey(c => c.Id);
        });
    }
}
