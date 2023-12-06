using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pedro.App.Data;
using Pedro.App.Extensions;
using Pedro.Data.Context;

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

        return services;
    }
}
