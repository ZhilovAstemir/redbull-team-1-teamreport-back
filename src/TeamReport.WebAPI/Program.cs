using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using redbull_team_1_teamreport_back.Data.Identity;
using redbull_team_1_teamreport_back.Data.Persistence;
using TeamReport.Domain.Mappers;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.MapperStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new BadRequestObjectResult(context.ModelState);
            result.StatusCode = StatusCodes.Status422UnprocessableEntity;

            return result;
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

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

// Configure the HTTP request pipeline.
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
