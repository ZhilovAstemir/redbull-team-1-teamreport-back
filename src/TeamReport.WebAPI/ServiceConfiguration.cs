﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using redbull_team_1_teamreport_back.Data.Repositories;
using redbull_team_1_teamreport_back.Data.Repositories.Interfaces;
using TeamReport.Domain.Services;
using TeamReport.Domain.Services.Interfaces;
using TeamReport.WebAPI;

namespace TeamReport.WebAPI;

[ExcludeFromCodeCoverage]
public static class ServiceConfiguration
{
    public static void AddDataRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
    }

    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ITeamService, TeamService>();
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

}