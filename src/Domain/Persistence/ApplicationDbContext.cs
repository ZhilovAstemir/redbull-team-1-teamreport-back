using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using redbull_team_1_teamreport_back.Domain.Entities;
using redbull_team_1_teamreport_back.Domain.Identity;

namespace redbull_team_1_teamreport_back.Domain.Persistence;

public class ApplicationDbContext:ApiAuthorizationDbContext<ApplicationUser>
{
#pragma warning disable CS8618
    public ApplicationDbContext(
#pragma warning restore CS8618
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Member> Members { get; set; } 
    public DbSet<Leadership> Leaderships { get; set; } 
    public DbSet<Report> Reports { get; set; } 
    public DbSet<Week> Weeks { get; set; } 
}
