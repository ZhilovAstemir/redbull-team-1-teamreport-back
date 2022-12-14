using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Identity;
using TeamReport.Data.Persistence;
using TeamReport.Domain.Infrastructures;
using TeamReport.Domain.Mappers;
using TeamReport.WebAPI.Extensions;
using TeamReport.WebAPI.Helpers;
using TeamReport.WebAPI.Mappers;

var builder = WebApplication.CreateBuilder(args);

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
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

var _aspnetCorsFromOut = Environment.GetEnvironmentVariable("ASPNET_CORS")?.Split(",") != null ? Environment.GetEnvironmentVariable("ASPNET_CORS")?.Split(",") : new string[] { };

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var _aspnetCorsDefault = new string[] { "http://localhost:3000", "http://localhost:7030" };

var corsDomains = _aspnetCorsDefault.Concat(_aspnetCorsFromOut).ToArray();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(corsDomains).AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDataRepositories();
builder.Services.AddDomainServices();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperDomain), typeof(MapperAPI));
builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection(nameof(EmailConfiguration)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();
app.UseAuthentication();
app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
