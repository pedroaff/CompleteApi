﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pedro.App.Data;
using Pedro.App.Extensions;
using Pedro.Data.Context;
using System.Text;

namespace Pedro.App.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration, string connString)
    {
        services.AddDbContext<ApplicationDbContext>(opts => opts.UseMySql(connString, ServerVersion.AutoDetect(connString)));

        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddErrorDescriber<IdentityPtBr>()
            .AddDefaultTokenProviders();


        // JWT

        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);

        var appSettings = appSettingsSection.Get<AppSettings>();
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = appSettings.ValidoEm,
                ValidIssuer = appSettings.Emissor,
            };
        });

        return services;
    }
}
