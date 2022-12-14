using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using redbull_team_1_teamreport_back.Data.Repositories;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Data.Repositories;
using TeamReport.Data.Repositories.Interfaces;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI.Models;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceConfiguration
{
    public static void AddDataRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IWeekRepository, WeekRepository>();
        services.AddScoped<ILeadershipRepository, LeadershipRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IReportService, ReportService>();
    }

    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "TeamReport", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Authorization: Bearer JWT",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
               });
        });
    }

    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);

        services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
        services.AddScoped<IValidator<MemberRegistrationRequest>, MemberRegistrationValidator>();
        services.AddScoped<IValidator<InviteMemberModelRequest>, InviteMemberValidator>();
        services.AddScoped<IValidator<CompanyRegistrationRequest>, CompanyRegistrationValidator>();
        services.AddScoped<IValidator<UpdateCompanyNameRequest>, UpdateCompanyNameValidator>();
        services.AddScoped<IValidator<ReportRequest>, ReportRequestValidator>();
    }
}
