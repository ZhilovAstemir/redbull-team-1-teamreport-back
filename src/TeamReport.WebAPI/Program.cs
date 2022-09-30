using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Identity;
using redbull_team_1_teamreport_back.Data.Persistence;
using TeamReport.Domain.Mappers;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.MapperStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(@"Data Source=localhost;Initial Catalog=WeeklyReport;Persist Security Info=True;User ID=sa;Password=Qwerty123");

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDataRepositories();
builder.Services.AddDomainServices();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperDomain), typeof(MapperAPI));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
