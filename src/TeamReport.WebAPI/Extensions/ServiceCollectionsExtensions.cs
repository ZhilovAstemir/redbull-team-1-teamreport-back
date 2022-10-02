using FluentValidation;
using Microsoft.OpenApi.Models;
using redbull_team_1_teamreport_back.Domain.Repositories;
using redbull_team_1_teamreport_back.Domain.Repositories.Interfaces;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using FluentValidation.AspNetCore;
using TeamReport.WebAPI.Models.Requests;
using TeamReport.WebAPI.Validators;

namespace TeamReport.WebAPI.Extensions;

public static class ServiceCollectionsExtensions
{
    public static void AddDataRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationServices, AuthorizationServices>();
        services.AddScoped<IMemberService, MemberService>();
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
    }
}
